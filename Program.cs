using System.Net.Mime;
using System.Text.Json;
using HouseInv.Config;
using HouseInv.Repositories;
using HouseInv.Repositories.Configurations.Cache;
using HouseInv.Repositories.Configurations.Schema;
using HouseInv.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
{
    // Postgresql config
    var postgresConfig = builder.Configuration.GetSection(nameof(PostgresConfig)).Get<PostgresConfig>();
    builder.Services.AddDbContext<HouseInvDbContext>(options =>
    {
        options.UseNpgsql(postgresConfig.ConnectionString)
            .ReplaceService<IModelCacheKeyFactory, DbSchemaAwareModelCacheKeyFactory>();
    });
    builder.Services.AddSingleton<IDbContextSchema>(new HouseInvDbSchema { Schema = postgresConfig.Schema });

    // MongoDB config
    BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
    BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

    var mongoDbConfig = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
    builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
    {
        return new MongoClient(mongoDbConfig.ConnectionString);
    });
    builder.Services.AddSingleton<IAsyncNotesRepository, MongoDbNotesRepository>();

    // Add internal Service classes
    builder.Services.AddScoped<IPersonsService, PersonsService>();
    builder.Services.AddScoped<IHousesService, HousesService>();
    builder.Services.AddScoped<ITenantsService, TenantsService>();
    builder.Services.AddScoped<IResourcesService, ResourcesService>();
    builder.Services.AddScoped<IPersonalResourcesService, PersonalResourcesService>();
    builder.Services.AddScoped<IAppliancesService, AppliancesService>();
    builder.Services.AddScoped<INotesService, NotesService>();

    // Controllers config
    builder.Services.AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks()
                    .AddMongoDb(
                        mongoDbConfig.ConnectionString,
                        name: "mongodb",
                        timeout: TimeSpan.FromSeconds(3),
                        tags: new[] { "ready" });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseExceptionHandler("/error");
    app.MapControllers();
    app.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = (_) => false
    });
    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = (check) => check.Tags.Contains("ready"),
        ResponseWriter = async (context, report) =>
        {
            var result = JsonSerializer.Serialize(
                new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                        duration = entry.Value.Duration.ToString()
                    })
                }
            );
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }
    });

    app.Run();
}