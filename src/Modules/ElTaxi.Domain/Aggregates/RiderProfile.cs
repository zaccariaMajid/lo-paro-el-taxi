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
    public PaymentMethods PreferredPaymentMethod { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalRides { get; set; }

    private RiderProfile() { }

    private RiderProfile(Guid userId, string fullName, PhoneNumber phoneNumber, PaymentMethods preferredPaymentMethod)
    {
        UserId = userId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        PreferredPaymentMethod = preferredPaymentMethod;
        AverageRating = 0;
        TotalRides = 0;
    }

    public static RiderProfile Create(Guid userId, string fullName, PhoneNumber phoneNumber, PaymentMethods preferredPaymentMethod)
    {
        return new RiderProfile(userId, fullName, phoneNumber, preferredPaymentMethod);
    }
    public void UpdatePreferredPaymentMethod(PaymentMethods newMethod)
    {
        PreferredPaymentMethod = newMethod;
    }
    public void UpdateRating(int newRating)
    {
        AverageRating = ((AverageRating * TotalRides) + newRating) / (TotalRides + 1);
        TotalRides += 1;
    }   
}
