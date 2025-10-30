using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Application;

namespace ElTaxi.Application.Interfaces;

public interface IUserInterface
{
    Task<Result<bool>> CreateDriverAsync(Guid userId, string name, string surname, string licenseNumber, string countryCode, string phoneNumber,string currentLatitude, string currentLongitude);
}
