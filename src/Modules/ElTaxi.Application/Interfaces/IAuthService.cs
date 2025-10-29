using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthLoginResponse>> LoginAsync(string email, string password, CancellationToken ct = default);
    Task<Result<AuthRegisterResponse>> RegisterAsync(string email, string password, UserRole role, CancellationToken ct = default);
}
