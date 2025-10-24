using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;

namespace ElTaxi.Infrastructure.Repositories;

public sealed class RideRepository : BaseRepository<Ride>, IRideRepository
{
    public RideRepository(ElTaxiDbContext context) : base(context)
    {
    }
}
