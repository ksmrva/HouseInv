using HouseInv.Models.Entities.Houses;
using HouseInv.Models.Entities.Persons;
using HouseInv.Models.Entities.Resources;
using HouseInv.Models.Entities.Resources.Personal;
using HouseInv.Models.Entities.Resources.Personal.Appliance;
using HouseInv.Models.Entities.Tenants;
using HouseInv.Repositories.Configurations.Entity;
using HouseInv.Repositories.Configurations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HouseInv.Repositories;

public class HouseInvDbContext : DbContext
{
    public string Schema { get; init; }
    public DbSet<Person> Person { get; set; } = null!;
    public DbSet<House> House { get; set; } = null!;
    public DbSet<Tenant> Tenant { get; set; } = null!;
    public DbSet<Appliance> Appliance { get; set; } = null!;
    public DbSet<Resource> Resource { get; set; } = null!;
    public DbSet<PersonalResource> PersonalResource { get; set; } = null!;

    public HouseInvDbContext(DbContextOptions<HouseInvDbContext> options, IDbContextSchema schema = null)
        : base(options)
    {
        Schema = schema?.Schema;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HouseInvDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration(Schema));
        modelBuilder.ApplyConfiguration(new HouseEntityTypeConfiguration(Schema));
        modelBuilder.ApplyConfiguration(new TenantEntityTypeConfiguration(Schema));
        modelBuilder.ApplyConfiguration(new ApplianceEntityTypeConfiguration(Schema));
        modelBuilder.ApplyConfiguration(new ResourceEntityTypeConfiguration(Schema));
        modelBuilder.ApplyConfiguration(new PersonalResourceEntityTypeConfiguration(Schema));
    }

    public void Detach(object entity)
    {
        Entry(entity).State = EntityState.Detached;
    }
}