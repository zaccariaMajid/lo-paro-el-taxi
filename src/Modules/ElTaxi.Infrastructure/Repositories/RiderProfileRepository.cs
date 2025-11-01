using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElTaxi.Infrastructure.Repositories;

public sealed class RiderProfileRepository : BaseRepository<RiderProfile>, IRiderProfileRepository
{
    public RiderProfileRepository(ElTaxiDbContext context) : base(context)
    {
    }


    public async Task<RiderProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default) => await _dbSet.FirstOrDefaultAsync(u => u.UserId == userId, ct);
}
