using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Domain.Aggregates;

public sealed class RiderProfile : Entity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public PhoneNumber PhoneNumber { get; set; } = null!;
    public PaymentTypes PreferredPaymentType { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalRides { get; set; }

    private RiderProfile() { }

    private RiderProfile(Guid userId, string fullName, PhoneNumber phoneNumber, PaymentTypes preferredPaymentType)
    {
        UserId = userId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        PreferredPaymentType = preferredPaymentType;
        AverageRating = 0;
        TotalRides = 0;
    }

    public static RiderProfile Create(Guid userId, string fullName, PhoneNumber phoneNumber, PaymentTypes preferredPaymentType)
    {
        return new RiderProfile(userId, fullName, phoneNumber, preferredPaymentType);
    }
    public void UpdatePreferredPaymentType(PaymentTypes newType)
    {
        PreferredPaymentType = newType;
    }
    public void UpdateRating(int newRating)
    {
        AverageRating = ((AverageRating * TotalRides) + newRating) / (TotalRides + 1);
        TotalRides += 1;
    }
}
