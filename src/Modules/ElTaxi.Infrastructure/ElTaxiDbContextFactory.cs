using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElTaxi.Infrastructure;

public class ElTaxiDbContextFactory : IDesignTimeDbContextFactory<ElTaxiDbContext>
{
    public ElTaxiDbContext CreateDbContext(string[] args)
    {
        var cs =
            Environment.GetEnvironmentVariable("ELTAXI__POSTGRES")
            ?? "Host=localhost;Database=ElTaxi;Username=postgres;Password=postgres"; // fallback

        var opts = new DbContextOptionsBuilder<ElTaxiDbContext>()
            .UseNpgsql(cs)
            .Options;

        return new ElTaxiDbContext(opts);
    }
}