using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Domain.Interfaces;

public interface IPricingRuleRepository : IBaseRepository<PricingRule>
{
    Task<PricingRule?> GetActiveByVehicleTypeAsync(VehicleType vehicleType, CancellationToken ct = default);
}
