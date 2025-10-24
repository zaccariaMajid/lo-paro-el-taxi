using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElTaxi.BuildingBlocks.Domain;

public interface IBaseRepository<TAggregate> where TAggregate : IAggregateRoot
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(TAggregate aggregate, CancellationToken ct = default);
    Task UpdateAsync(TAggregate aggregate, CancellationToken ct = default);
    void DeleteAsync(TAggregate aggregate, CancellationToken ct = default);
    Task<IEnumerable<TAggregate>> FindAsync( Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
