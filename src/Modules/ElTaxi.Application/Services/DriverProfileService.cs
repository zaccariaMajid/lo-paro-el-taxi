using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Application.Services;

public class DriverProfileService : IDriverProfileInterface
{
    private readonly IDriverProfileRepository _driverProfileRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DriverProfileService(IDriverProfileRepository driverProfileRepository, IUnitOfWork unitOfWork)
    {
        _driverProfileRepository = driverProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DriverCreationResponse>> CreateDriverAsync(Guid userId, string name, string surname, string licenseNumber, string countryCode, string phoneNumber, double currentLatitude, double currentLongitude, CancellationToken ct = default)
    {
        DriverProfile newDriver;
        try
        {
            var validationResult = validateCreation(userId, name, surname, licenseNumber, currentLatitude, currentLongitude);
            if (!validationResult.IsSuccess)
                return Result<DriverCreationResponse>.Fail("Failed to validate Driver creation request");

            var existsDriver = await _driverProfileRepository.GetByUserIdAsync(userId) is not null;
            if (existsDriver)
                return Result<DriverCreationResponse>.Fail("Current user has already a driver profile");

            var existsLicenseNumber = await _driverProfileRepository.GetByLicenseNumberAsync(licenseNumber) is not null;
            if (existsLicenseNumber)
                return Result<DriverCreationResponse>.Fail("Current License Number is already registered");

            var phoneNumberVo = PhoneNumber.Create(countryCode, phoneNumber);

            newDriver = DriverProfile.Create(
                userId,
                $"{name.Trim()} {surname.Trim()}",
                licenseNumber,
                phoneNumberVo,
                currentLatitude,
                currentLongitude
            );

            await _driverProfileRepository.AddAsync(newDriver);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<DriverCreationResponse>.Fail($"Failed to create driver profile: {ex.Message}");
        }
        return Result<DriverCreationResponse>.Success(new DriverCreationResponse(newDriver.Id));
    }

    private Result<bool> validateCreation(Guid? userId, string name, string surname, string licenseNumber, double currentLatitude, double currentLongitude)
    {
        if (name is null)
            return Result<bool>.Fail("Name cannot be null");
        if (surname is null)
            return Result<bool>.Fail("Surname cannot be null");
        if (licenseNumber is null)
            return Result<bool>.Fail("LicenseNumber cannot be null");

        return Result<bool>.Success(true);
    }
}

public record DriverCreationResponse(Guid driverId);
