using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ElTaxi.Infrastructure.Configurations;

public class DriverProfileConfiguration : IEntityTypeConfiguration<DriverProfile>
{
    public void Configure(EntityTypeBuilder<DriverProfile> builder)
    {
        builder.Property(dp => dp.UserId)
            .IsRequired();

        builder.Property(dp => dp.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(dp => dp.LicenseNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(rp => rp.PhoneNumber, phoneBuilder =>
        {
            phoneBuilder.Property(pn => pn.CountryCode)
                .HasColumnName("CountryCode")
                .IsRequired()
                .HasMaxLength(5);
            phoneBuilder.Property(pn => pn.Number)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(15);
        });

        builder.Property(dp => dp.AverageRating)
            .IsRequired();

        builder.Property(dp => dp.TotalRides)
            .IsRequired();

        builder.Property(dp => dp.CurrentLatitude)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(dp => dp.CurrentLongitude)
            .IsRequired()
            .HasMaxLength(20);
            
        builder.Property(dp => dp.LastPingAt)
            .IsRequired();
    }
}