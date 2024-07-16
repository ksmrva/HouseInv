using HouseInv.Models.Entities.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Repositories.Configurations.Entity;

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
            builder.ToTable(nameof(HouseInvDbContext.Resource), _schema);
        }
        builder.HasKey(b => b.Id);
        // builder.Property(b => b.Id)
        //     .ValueGeneratedNever();
    }
}