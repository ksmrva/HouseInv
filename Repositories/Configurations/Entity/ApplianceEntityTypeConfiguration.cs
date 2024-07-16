using HouseInv.Models.Entities.Resources.Personal.Appliance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Repositories.Configurations.Entity;

public class ApplianceEntityTypeConfiguration : IEntityTypeConfiguration<Appliance>
{
    private readonly string _schema;

    public ApplianceEntityTypeConfiguration(string schema)
    {
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Appliance> builder)
    {
        if (!string.IsNullOrWhiteSpace(_schema))
        {
            builder.ToTable(nameof(HouseInvDbContext.Appliance), _schema);
        }
        builder.HasKey(b => b.Id);
        // builder.Property(b => b.Id)
        //     .ValueGeneratedNever();
    }
}