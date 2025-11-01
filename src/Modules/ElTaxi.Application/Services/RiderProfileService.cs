using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Application.Services;

public class RiderProfileService : IRiderProfileService
{
    private readonly IRiderProfileRepository _riderProfileRepository = null!;
    private readonly IUnitOfWork _unitOfWork = null!;
    public RiderProfileService(IRiderProfileRepository riderProfileRepository, IUnitOfWork unitOfWork)
    {
        _riderProfileRepository = riderProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RiderCreationResponse>> CreateRiderAsync(Guid userId, string name, string surname, string countryCode, string phoneNumber, PaymentTypes preferredPaymentType, CancellationToken ct = default)
    {
        RiderProfile newRider;
        try
        {
            var validationResult = validateCreation(name, surname, preferredPaymentType);
            if (!validationResult.IsSuccess)
                return Result<RiderCreationResponse>.Fail($"Failed creating Rider: {validationResult.Error}");

            var existsDriver = await _riderProfileRepository.GetByUserIdAsync(userId) is not null;
            if (existsDriver)
                return Result<RiderCreationResponse>.Fail("Current user has already a driver profile");

            var phoneNumberVo = PhoneNumber.Create(countryCode, phoneNumber);

            newRider = RiderProfile.Create(userId, $"{name} {surname}", phoneNumberVo, preferredPaymentType);

            await _riderProfileRepository.AddAsync(newRider);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            return Result<RiderCreationResponse>.Fail($"Failed creating Rider: {ex.Message}");
        }

        return Result<RiderCreationResponse>.Success(new RiderCreationResponse(newRider.Id));
    }   

    protected Result<bool> validateCreation(string name, string surname, PaymentTypes preferredPaymentType)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<bool>.Fail("Name cannot be null");
        if (string.IsNullOrWhiteSpace(surname))
            return Result<bool>.Fail("Surname cannot be null");
        if (!Enum.IsDefined(preferredPaymentType))
            return Result<bool>.Fail("Preferred payment type unknown");

        return Result<bool>.Success(true);
    }
}

public record RiderCreationResponse(Guid riderId);