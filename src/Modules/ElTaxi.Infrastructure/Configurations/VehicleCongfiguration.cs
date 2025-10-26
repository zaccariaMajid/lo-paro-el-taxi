using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class VehicleCongfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.Property(v => v.Make)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Year)
            .IsRequired();

        builder.Property(v => v.LicensePlate)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.Seats)
            .IsRequired();

        builder.Property(v => v.Color)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Type)
            .IsRequired();
    }
}
