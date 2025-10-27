using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class PingConfiguration : IEntityTypeConfiguration<Ping>
{
    public void Configure(EntityTypeBuilder<Ping> builder)
    {
        builder.ToTable("Pings");

        builder.Property(p => p.DriverId)
            .IsRequired();

        builder.Property(p => p.Latitude)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Longitude)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Speed)
            .IsRequired();

        builder.Property(p => p.Heading)
            .IsRequired();

        builder.Property(p => p.Accuracy)
            .IsRequired();

        builder.Property(p => p.SentAt)
            .IsRequired();
    }
}
