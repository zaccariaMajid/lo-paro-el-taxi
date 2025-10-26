using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class RiderProfileConfiguration : IEntityTypeConfiguration<RiderProfile>
{

    public void Configure(EntityTypeBuilder<RiderProfile> builder)
    {
        builder.ToTable("RiderProfiles");

        builder.Property(rp => rp.UserId)
            .IsRequired();

        builder.Property(rp => rp.FullName)
            .IsRequired()
            .HasMaxLength(200);

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

        builder.Property(rp => rp.PreferredPaymentType)
            .IsRequired();

        builder.Property(rp => rp.AverageRating)
            .IsRequired()
            .HasColumnType("decimal(3,2)")
            .HasDefaultValue(0);

        builder.Property(rp => rp.TotalRides)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
