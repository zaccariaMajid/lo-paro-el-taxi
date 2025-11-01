using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElTaxi.Infrastructure.Repositories;

public sealed class PricingRuleRepository : BaseRepository<PricingRule>, IPricingRuleRepository
{
    public PricingRuleRepository(ElTaxiDbContext context) : base(context)
    {
    }

    public async Task<PricingRule?> GetActiveByVehicleTypeAsync(VehicleType vehicleType, CancellationToken ct = default) 
        => await _dbSet.FirstOrDefaultAsync(u => u.VehicleType == vehicleType && DateTime.Now > u.ActiveFrom && u.ActiveTo < DateTime.Now, ct);
}
