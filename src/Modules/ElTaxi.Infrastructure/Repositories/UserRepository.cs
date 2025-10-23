using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ElTaxi.Infrastructure.Repositories;

public class BaseRepository<TAggregate> : IRepository<TAggregate> where TAggregate : Entity, IAggregateRoot
{
    protected readonly ElTaxiDbContext _context;
    protected readonly DbSet<TAggregate> _dbSet;
    public BaseRepository(ElTaxiDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TAggregate>();
    }

    public virtual async Task AddAsync(TAggregate aggregate, CancellationToken ct = default) => await _dbSet.AddAsync(aggregate, ct);
    
    public virtual void DeleteAsync(TAggregate aggregate, CancellationToken ct = default) => _dbSet.Remove(aggregate);

    public virtual async Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken ct = default) => await _dbSet.FirstOrDefaultAsync(e => e.Id == id, ct);

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TAggregate aggregate, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
