using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;

namespace ElTaxi.Domain.Interfaces;

public interface IDriverProfileRepository : IBaseRepository<DriverProfile>
{
    Task<DriverProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<DriverProfile?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken ct = default);
}
