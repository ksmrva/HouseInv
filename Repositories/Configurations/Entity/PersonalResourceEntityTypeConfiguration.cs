using HouseInv.Models.Entities.Resources;
using HouseInv.Models.Entities.Resources.Personal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Repositories.Configurations.Entity;

public class PersonalResourceEntityTypeConfiguration : IEntityTypeConfiguration<PersonalResource>
{
    private readonly string _schema;

    public PersonalResourceEntityTypeConfiguration(string schema)
    {
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<PersonalResource> builder)
    {
        if (!string.IsNullOrWhiteSpace(_schema))
        {
            builder.ToTable(nameof(HouseInvDbContext.PersonalResource), _schema);
        }
        builder.HasKey(b => b.Id);
        // builder.Property(b => b.Id)
        //     .ValueGeneratedNever();
    }
}