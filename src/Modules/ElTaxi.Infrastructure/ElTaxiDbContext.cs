using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ElTaxi.Infrastructure;

public sealed class ElTaxiDbContext : DbContext
{
    public ElTaxiDbContext(DbContextOptions<ElTaxiDbContext> options)
        : base(options)
    {
    }

    public DbSet<DriverProfile> DriverProfiles { get; set; } = null!;
    public DbSet<Notification> Notification { get; set; } = null!;
    public DbSet<Payment> Payment { get; set; } = null!;
    public DbSet<Ping> Pings { get; set; } = null!;
    public DbSet<PricingRule> PricingRule { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Ride> Rides { get; set; } = null!;
    public DbSet<RiderProfile> RiderProfiles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>().UseTpcMappingStrategy();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElTaxiDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
        .Where(t => typeof(Entity).IsAssignableFrom(t.ClrType)
                    && t.ClrType != typeof(Entity))) // evita la base
        {
            var builder = modelBuilder.Entity(entityType.ClrType);

            builder.Property(nameof(Entity.CreatedAt)).IsRequired();
            builder.Property(nameof(Entity.UpdatedAt));
            builder.Property(nameof(Entity.RowVersion)).IsRowVersion();
        }
        base.OnModelCreating(modelBuilder);
    }
}
