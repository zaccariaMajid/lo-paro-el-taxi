using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Application.Interfaces;

public interface IPricingRuleService
{
    Task<Result<PricingRuleCreationResponse>> CreatePricinRuleAsync(VehicleType vehicleType, decimal baseFareInCents, decimal costPerKmInCents, decimal costPerMinuteInCents, decimal surgeMultiplier, DateTime activeFrom, DateTime activeTo);
}
