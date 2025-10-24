using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElTaxi.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ElTaxiDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) => await _dbSet.FirstOrDefaultAsync(u => u.Email.Value == email, ct);
}
