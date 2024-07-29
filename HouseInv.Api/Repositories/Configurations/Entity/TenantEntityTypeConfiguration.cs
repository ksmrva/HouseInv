using HouseInv.Api.Models.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseInv.Api.Repositories.Configurations.Entity;

public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
{
    private readonly string _schema;

    public TenantEntityTypeConfiguration(string schema)
    {
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        if (!string.IsNullOrWhiteSpace(_schema))
        {
            builder.ToTable(nameof(HouseInvDbContext.Tenant).ToLower(), _schema);
        }
        builder.HasKey(b => b.Id);
    }
}