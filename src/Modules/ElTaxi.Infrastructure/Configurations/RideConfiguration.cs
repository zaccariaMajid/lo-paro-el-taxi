using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class RideConfiguration : IEntityTypeConfiguration<Ride>
{
    public void Configure(EntityTypeBuilder<Ride> builder)
    {
        builder.ToTable("Rides");

        builder.Property(r => r.RiderId)
            .IsRequired();

        builder.Property(r => r.DriverId)
            .IsRequired();

        builder.OwnsOne(r => r.PickUpAddress, pa =>
        {
            pa.Property(a => a.Street).IsRequired().HasMaxLength(200);
            pa.Property(a => a.City).IsRequired().HasMaxLength(100);
            pa.Property(a => a.State).IsRequired().HasMaxLength(100);
            pa.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
            pa.Property(a => a.Country).IsRequired().HasMaxLength(100);
        });

        builder.Property(r => r.PickupLatitude)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.PickupLongitude)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(r => r.DropOffAddress, da =>
        {
            da.Property(a => a.Street).IsRequired().HasMaxLength(200);
            da.Property(a => a.City).IsRequired().HasMaxLength(100);
            da.Property(a => a.State).IsRequired().HasMaxLength(100);
            da.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
            da.Property(a => a.Country).IsRequired().HasMaxLength(100);
        });

        builder.Property(r => r.DropOffLatitude)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.DropOffLongitude)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.RequestedAt)
            .IsRequired();

        builder.Property(r => r.StartedAt)
            .IsRequired(false);

        builder.Property(r => r.CompletedAt)
            .IsRequired(false);

        builder.Property(r => r.CanceledAt)
            .IsRequired(false);

        builder.Property(r => r.Status)
            .IsRequired();

        builder.Property(r => r.DistanceInKm)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.EstimatedDurationInMinutes)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.EstimatedPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.FinalPice)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.PaymentId);

        builder.Property(r => r.DriverRating);

        builder.Property(r => r.RiderRating);
    }
}
