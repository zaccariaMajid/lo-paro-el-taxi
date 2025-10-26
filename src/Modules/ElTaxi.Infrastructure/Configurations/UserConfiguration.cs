using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElTaxi.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(100);
        });
        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired();

        builder.Property(u => u.Status)
            .IsRequired();
        builder.Property(u => u.lastLoginAt)
            .IsRequired(false);

    }
}
