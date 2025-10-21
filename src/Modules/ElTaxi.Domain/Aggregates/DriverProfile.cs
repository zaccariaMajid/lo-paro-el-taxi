using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Domain.Aggregates;

public sealed class DriverProfile : Entity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public PhoneNumber PhoneNumber { get; set; } = null!;
    public decimal AverageRating { get; set; }
    public int TotalRides { get; set; }
    public Guid? VehicleId { get; set; }
    public string CurrentLatitude { get; set; } = null!;
    public string CurrentLongitude { get; set; } = null!;
    public DateTime LastPingAt { get; set; }

    private DriverProfile() { }

    private DriverProfile(Guid userId, string fullName, PhoneNumber phoneNumber, string currentLatitude, string currentLongitude)
    {
        UserId = userId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        AverageRating = 0;
        TotalRides = 0;
        CurrentLatitude = currentLatitude;
        CurrentLongitude = currentLongitude;
        LastPingAt = DateTime.UtcNow;
    }

    public static DriverProfile Create(Guid userId, string fullName, PhoneNumber phoneNumber, string currentLatitude, string currentLongitude)
    {
        return new DriverProfile(userId, fullName, phoneNumber, currentLatitude, currentLongitude);
    }

    public void UpdateLocation(string latitude, string longitude)
    {
        CurrentLatitude = latitude;
        CurrentLongitude = longitude;
        LastPingAt = DateTime.UtcNow;
    }

    public void AssignVehicle(Guid vehicleId)
    {
        VehicleId = vehicleId;
    }

    public void UpdateRating(int newRating)
    {
        AverageRating = ((AverageRating * TotalRides) + newRating) / (TotalRides + 1);
        TotalRides += 1;
    }
}
