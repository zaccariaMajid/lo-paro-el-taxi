using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Application;

namespace ElTaxi.Application.Interfaces;

public interface ITokenService
{
    Result<string> GenerateToken(Guid userId, string email, string role);
}
