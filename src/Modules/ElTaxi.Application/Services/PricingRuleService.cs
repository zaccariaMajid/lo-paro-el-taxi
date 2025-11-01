using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.Interfaces;

namespace ElTaxi.Application.Services;

public class PricingRuleService : IPricingRuleService
{
    private readonly IPricingRuleRepository _pricingRuleRepository;
    private readonly IUnitOfWork _unitOfWork;
    public PricingRuleService(IPricingRuleRepository pricingRuleRepository, IUnitOfWork unitOfWork)
    {
        _pricingRuleRepository = pricingRuleRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<PricingRuleCreationResponse>> CreatePricinRuleAsync(VehicleType vehicleType, decimal baseFareInCents, decimal costPerKmInCents, decimal costPerMinuteInCents, decimal surgeMultiplier, DateTime activeFrom, DateTime activeTo)
    {
        if (!Enum.IsDefined(vehicleType))
            return Result<PricingRuleCreationResponse>.Fail($"{nameof(vehicleType)} unknown");

        var newPricingRule = PricingRule.Create(vehicleType, baseFareInCents, costPerKmInCents, costPerMinuteInCents, surgeMultiplier, activeFrom, activeTo);

        await _pricingRuleRepository.AddAsync(newPricingRule);
        await _unitOfWork.SaveChangesAsync();

        return Result<PricingRuleCreationResponse>.Success(new PricingRuleCreationResponse(newPricingRule.Id));
    }
}

public record PricingRuleCreationResponse(Guid pricingRuleId);
