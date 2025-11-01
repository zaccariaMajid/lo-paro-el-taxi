using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Application.Services;

public class RideService : IRideService
{
    private readonly IRideRepository _rideRepository;
    private readonly IRiderProfileRepository _riderProfileRepository;
    private readonly IDriverProfileRepository _driverProfileRepository;
    private readonly IPricingRuleRepository _pricingRuleRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RideService(IRideRepository rideRepository, IUnitOfWork unitOfWork, IRiderProfileRepository riderProfileRepository, IDriverProfileRepository driverProfileRepository, IPricingRuleRepository pricingRuleRepository)
    {
        _rideRepository = rideRepository;
        _unitOfWork = unitOfWork;
        _riderProfileRepository = riderProfileRepository;
        _driverProfileRepository = driverProfileRepository;
        _pricingRuleRepository = pricingRuleRepository;
    }

    public Result<Address> CreateAddress(string street, string city, string state, string zipCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            return Result<Address>.Fail($"{nameof(street)} cannot be null");
        if (string.IsNullOrWhiteSpace(city))
            return Result<Address>.Fail($"{nameof(city)} cannot be null");
        if (string.IsNullOrWhiteSpace(state))
            return Result<Address>.Fail($"{nameof(state)} cannot be null");
        if (string.IsNullOrWhiteSpace(zipCode))
            return Result<Address>.Fail($"{nameof(zipCode)} cannot be null");
        if (string.IsNullOrWhiteSpace(country))
            return Result<Address>.Fail($"{nameof(country)} cannot be null");

        Address address;
        try
        {
            address = Address.Create(street, city, state, zipCode, country);
        }
        catch (Exception ex)
        {
            return Result<Address>.Fail($"Failed creating Address: {ex.Message}");
        }
        return Result<Address>.Success(address);
    }
    public async Task<Result<RideCreationResponse>> CreateRideAsync(Guid riderId, Guid driverId, Address pickUpAddress, double pickupLatitude, double pickupLongitude, Address dropOffAddress, double dropOffLatitude, double dropOffLongitude, decimal distanceInKm, decimal estimatedDurationInMinutes, decimal estimatedPrice)
    {
        var riderExists = await _riderProfileRepository.GetByIdAsync(riderId) is not null;

        if (!riderExists)
            return Result<RideCreationResponse>.Fail("Rider profile not found");

        var driverExists = await _driverProfileRepository.GetByIdAsync(driverId) is not null;

        if (!driverExists)
            return Result<RideCreationResponse>.Fail("Driver profile not found");

        var activePricingRuleForVehicleType = await _pricingRuleRepository.GetActiveByVehicleTypeAsync(vehicleType);

        var newRide = Ride.Create(riderId, driverId, pickUpAddress, pickupLatitude, pickupLongitude, dropOffAddress, dropOffLatitude, dropOffLongitude, distanceInKm, estimatedDurationInMinutes, estimatedPrice);

        await _rideRepository.AddAsync(newRide);
        await _unitOfWork.SaveChangesAsync();

        return Result<RideCreationResponse>.Success(new RideCreationResponse(newRide.Id, newRide.EstimatedPrice));
    }
}

public record RideCreationResponse(Guid rideId, decimal estimatedPrice);
