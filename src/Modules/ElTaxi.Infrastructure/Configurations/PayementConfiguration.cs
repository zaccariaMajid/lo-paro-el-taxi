using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class PayementConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.Property(p => p.AmountInCents)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Currency)
            .IsRequired()
            .HasMaxLength(3);
        builder.Property(p => p.Method)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.TransactionId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.PaidAt)
            .IsRequired();

        builder.Property(p => p.RefundedAt)
            .IsRequired(false);
    }
}
