using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Domain.Aggregates;

public sealed class Ride : Entity, IAggregateRoot
{
    public Guid RiderId { get; set; }
    public Guid DriverId { get; set; }
    public Address PickUpAddress { get; set; } = null!;
    public string PickupLatitude { get; set; } = null!;
    public string PickupLongitude { get; set; } = null!;
    public Address DropOffAddress { get; set; } = null!;
    public string DropOffLatitude { get; set; } = null!;
    public string DropOffLongitude { get; set; } = null!;
    public DateTime RequestedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CanceledAt { get; set; }
    public RideStatus Status { get; set; }
    public decimal DistanceInKm { get; set; }
    public decimal EstimatedDurationInMinutes { get; set; }
    public decimal EstimatedPrice { get; set; }
    public decimal FinalPice { get; set; }
    public Guid PaymentId { get; set; }
    public int DriverRating { get; set; }
    public int RiderRating { get; set; }
    private Ride() { }
    private Ride(Guid riderId, Guid driverId, Address pickUpAddress, string pickupLatitude, string pickupLongitude, Address dropOffAddress, string dropOffLatitude, string dropOffLongitude, decimal distanceInKm, decimal estimatedDurationInMinutes, decimal estimatedPrice)
    {
        RiderId = riderId;
        DriverId = driverId;
        PickUpAddress = pickUpAddress;
        PickupLatitude = pickupLatitude;
        PickupLongitude = pickupLongitude;
        DropOffAddress = dropOffAddress;
        DropOffLatitude = dropOffLatitude;
        DropOffLongitude = dropOffLongitude;
        RequestedAt = DateTime.UtcNow;
        Status = RideStatus.Requested;
        DistanceInKm = distanceInKm;
        EstimatedDurationInMinutes = estimatedDurationInMinutes;
        EstimatedPrice = estimatedPrice;
    }
    public static Ride Create(
        Guid riderId,
        Guid driverId,
        Address pickUpAddress,
        string pickupLatitude,
        string pickupLongitude,
        Address dropOffAddress,
        string dropOffLatitude,
        string dropOffLongitude,
        decimal distanceInKm,
        decimal estimatedDurationInMinutes,
        decimal estimatedPrice)
    {
        return new Ride(riderId, driverId, pickUpAddress, pickupLatitude, pickupLongitude, dropOffAddress, dropOffLatitude, dropOffLongitude, distanceInKm, estimatedDurationInMinutes, estimatedPrice);
    }

    public void StartRide()
    {
        if (Status != RideStatus.Requested)
            throw new Exception("Ride cannot be started in its current status.");

        Status = RideStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void CompleteRide(decimal finalPrice)
    {
        if (Status != RideStatus.InProgress)
            throw new Exception("Ride cannot be completed in its current status.");

        Status = RideStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        FinalPice = finalPrice;
    }

    public void CancelRide()
    {
        if (Status == RideStatus.Completed || Status == RideStatus.Canceled)
            throw new Exception("Ride cannot be canceled in its current status.");

        Status = RideStatus.Canceled;
        CanceledAt = DateTime.UtcNow;
    }

    public void RateDriver(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));

        DriverRating = rating;
    }

    public void RateRider(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));

        RiderRating = rating;
    }
}
