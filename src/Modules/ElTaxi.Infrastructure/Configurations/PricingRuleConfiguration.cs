using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public class PricingRuleConfiguration : IEntityTypeConfiguration<PricingRule>
{
    public void Configure(EntityTypeBuilder<PricingRule> builder)
    {
        builder.ToTable("PricingRules");
        
        builder.Property(pr => pr.VehicleType)
            .IsRequired();

        builder.Property(pr => pr.BaseFareInCents)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(pr => pr.CostPerKmInCents)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(pr => pr.CostPerMinuteInCents)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(pr => pr.SurgeMultiplier)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(pr => pr.ActiveFrom)
            .IsRequired();

        builder.Property(pr => pr.ActiveTo)
            .IsRequired();
    }
}
