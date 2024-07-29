using HouseInv.Api.Models.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Api.Repositories.Configurations.Entity;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    private readonly string _schema;

    public PersonEntityTypeConfiguration(string schema)
    {
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Person> builder)
    {
        if (!string.IsNullOrWhiteSpace(_schema))
        {
            builder.ToTable(nameof(HouseInvDbContext.Person).ToLower(), _schema);
        }
        builder.HasKey(b => b.Id);
    }
}