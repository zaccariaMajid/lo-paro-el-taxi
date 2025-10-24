using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;

namespace ElTaxi.Infrastructure.Repositories;

public sealed class PricingRuleRepository : BaseRepository<PricingRule>, IPricingRuleRepository
{
    public PricingRuleRepository(ElTaxiDbContext context) : base(context)
    {
    }
}
