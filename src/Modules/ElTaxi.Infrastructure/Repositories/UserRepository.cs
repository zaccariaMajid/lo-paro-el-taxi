using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;

namespace ElTaxi.Infrastructure.Repositories;

public class BaseRepository<TAggregate> : IRepository<TAggregate> where TAggregate : IAggregateRoot
{
    protected readonly ElTaxiDbContext _context;
    public BaseRepository(ElTaxiDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(TAggregate aggregate, CancellationToken ct = default) => await _context.AddAsync(aggregate, ct);


    public Task DeleteAsync(User aggregate, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User aggregate, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
