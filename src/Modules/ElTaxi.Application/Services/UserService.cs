using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.Domain.Aggregates;

namespace ElTaxi.Application.Services;

public class UserService : IUserInterface
{
    public async Task<Result<bool>> CreateDriverAsync(Guid userId, string name, string surname, string licenseNumber, string countryCode,string phoneNumber, string currentLatitude, string currentLongitude)
    {
        if (validateCreation(userId, name, surname, licenseNumber, currentLatitude, currentLongitude).Value)
        {
            return Result<bool>.Fail("Fail");
        }
        return Result<bool>.Success(true);
    }
    
    private Result<bool> validateCreation(Guid? userId, string name, string surname, string licenseNumber, string currentLatitude, string currentLongitude)
    {
        if (userId is null)
            return Result<bool>.Fail("UserId cannot be null");
        if (name is null)
            return Result<bool>.Fail("Name cannot be null");
        if (surname is null)
            return Result<bool>.Fail("Surname cannot be null");
        if (licenseNumber is null)
            return Result<bool>.Fail("LicenseNumber cannot be null");
        if (currentLatitude is null)
            return Result<bool>.Fail("CurrentLatitude cannot be null");
        if (currentLongitude is null)
            return Result<bool>.Fail("CurrentLongitude cannot be null");
        
        return Result<bool>.Success(true);
    }
}
