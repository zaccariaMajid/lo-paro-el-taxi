using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;

namespace ElTaxi.Infrastructure.Repositories;

public sealed class DriverProfileRepository : BaseRepository<DriverProfile>, IDriverProfileRepository
{
    public DriverProfileRepository(ElTaxiDbContext context) : base(context)
    {
    }
}
