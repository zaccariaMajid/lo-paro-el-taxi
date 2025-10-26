using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.Infrastructure;

public class UnitOfWork
{
    private readonly ElTaxiDbContext _db;
    public UnitOfWork(ElTaxiDbContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
