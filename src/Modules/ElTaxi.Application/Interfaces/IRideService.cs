using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Application.Interfaces;

public interface IRideService
{
    Task<bool> CreateRideAsync(Guid riderId, Guid driverId, Address pickUpAddress, string pickupLatitude, string pickupLongitude, Address dropOffAddress, string dropOffLatitude, string dropOffLongitude, decimal distanceInKm, decimal estimatedDurationInMinutes, decimal estimatedPrice);
}
