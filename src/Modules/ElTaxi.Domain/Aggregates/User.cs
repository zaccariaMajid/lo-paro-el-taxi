using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Domain.Aggregates;

public sealed class User : Entity, IAggregateRoot
{
    public Email Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTime? lastLoginAt { get; set; }
    public UserStatus Status { get; set; }

    private User() { }

    private User(Email email, string passwordHash, UserRole role, UserStatus status)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Status = status;
    }

    public static User Create(Email email, string passwordHash, UserRole role, UserStatus status)
    {
        return new User(email, passwordHash, role, status);
    }

    public void ChangeStatus(UserStatus status)
    {
        if (Status == status)
            throw new Exception($"User already in status {Enum.GetName(typeof(UserStatus), status)}");

        Status = UserStatus.Active;
    }
}
