using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        
        builder.Property(r => r.RideId)
            .IsRequired();

        builder.Property(r => r.ReviewerId)
            .IsRequired();

        builder.Property(r => r.RevieweeId)
            .IsRequired();

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.Comments)
            .IsRequired()
            .HasMaxLength(1000);
    }
}
