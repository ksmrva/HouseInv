using HouseInv.Models.Entities.Houses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Repositories.Configurations.Entity;

public class HouseEntityTypeConfiguration : IEntityTypeConfiguration<House>
{
    private readonly string _schema;

    public HouseEntityTypeConfiguration(string schema)
    {
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<House> builder)
    {
        if (!string.IsNullOrWhiteSpace(_schema))
        {
            builder.ToTable(nameof(HouseInvDbContext.House), _schema);
        }
        builder.HasKey(b => b.Id);
        // builder.Property(b => b.Id)
        //     .ValueGeneratedNever();
    }
}