using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.BuildingBlocks.Domain;

public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(TAggregate aggregate, CancellationToken ct = default);
}
