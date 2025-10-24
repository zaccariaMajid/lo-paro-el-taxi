using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using Ping = ElTaxi.Domain.Aggregates.Ping;

namespace ElTaxi.Domain.Interfaces;

public interface IPingRepository : IBaseRepository<Ping>
{
    
}
