using HouseInv.Api.Models.Entities.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Api.Repositories.Configurations.Entity;

public class ResourceEntityTypeConfiguration : IEntityTypeConfiguration<Resource>
{
    private readonly string _schema;

    public ResourceEntityTypeConfiguration(string schema)
    {
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        if (!string.IsNullOrWhiteSpace(_schema))
        {
            builder.ToTable(nameof(HouseInvDbContext.Resource).ToLower(), _schema);
        }
        builder.HasKey(b => b.Id);
    }
}