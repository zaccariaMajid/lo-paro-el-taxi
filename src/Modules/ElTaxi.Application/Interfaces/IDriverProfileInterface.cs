using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Application;

namespace ElTaxi.Application.Interfaces;

public interface IDriverProfileInterface
{
    Task<Result<DriverCreationResponse>> CreateDriverAsync(Guid userId, string name, string surname, string licenseNumber, string countryCode, string phoneNumber,double currentLatitude, double currentLongitude, CancellationToken ct = default);
}
