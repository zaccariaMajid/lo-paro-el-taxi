using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElTaxi.Infrastructure.Repositories;

public sealed class DriverProfileRepository : BaseRepository<DriverProfile>, IDriverProfileRepository
{
    public DriverProfileRepository(ElTaxiDbContext context) : base(context)
    {
    }
    public async Task<DriverProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default) => await _dbSet.FirstOrDefaultAsync(u => u.UserId == userId, ct);
    public async Task<DriverProfile?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken ct = default) => await _dbSet.FirstOrDefaultAsync(u => u.LicenseNumber == licenseNumber, ct);
}
