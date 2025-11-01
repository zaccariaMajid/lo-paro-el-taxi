using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.Testing.Extensions.VSTestBridge.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElTaxi.UnitTests;

[TestClass]
public class DriverProfileServiceTest
{
    private readonly Mock<IDriverProfileRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private DriverProfileService _driverProfileService = null!;

    [TestInitialize]
    public void Setup()
    {
        _driverProfileService = new DriverProfileService(_repoMock.Object, _unitOfWorkMock.Object);
    }

    [TestMethod]
    public async Task ShouldCorrectlyCreateDriverProfile_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            licenseNumber = "AB149003LL",
            countryCode = "39",
            phoneNumber = "3333333333",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)null);
        _repoMock.Setup(repo => repo.GetByLicenseNumberAsync(request.licenseNumber, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)null);
        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, request.name, request.surname, request.licenseNumber, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateDriverProfileIfNameIsNull_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            surname = "Majid",
            licenseNumber = "AB149003LL",
            countryCode = "39",
            phoneNumber = "3333333333",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };

        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, null!, request.surname, request.licenseNumber, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateDriverProfileIfSurnameIsNull_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Majid",
            licenseNumber = "AB149003LL",
            countryCode = "39",
            phoneNumber = "3333333333",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };

        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, request.name, null!, request.licenseNumber, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateDriverProfileIfLicenseNumberIsNull_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Majid",
            surname = "Zaccaria",
            licenseNumber = "AB149003LL",
            countryCode = "39",
            phoneNumber = "3333333333",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };

        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, request.name, request.surname, null!, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateDriverProfileIfDriverForUserAlreadyExits_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            licenseNumber = "AB149003LL",
            countryCode = "39",
            phoneNumber = "3333333333",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };
        var phoneNumberVo = PhoneNumber.Create(request.countryCode, request.phoneNumber);
        var existingDriver = DriverProfile.Create(
                request.userId,
                $"{request.name.Trim()} {request.surname.Trim()}",
                request.licenseNumber,
                phoneNumberVo,
                request.currentLatitude,
                request.currentLongitude
            );

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)existingDriver);
        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, request.name, request.surname, request.licenseNumber, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateDriverProfileIfLicenseNumberIsAlreadyRegistered_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            licenseNumber = "AB149003LL",
            countryCode = "39",
            phoneNumber = "3333333333",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };
        var phoneNumberVo = PhoneNumber.Create(request.countryCode, request.phoneNumber);
        var existingDriver = DriverProfile.Create(
                request.userId,
                $"{request.name.Trim()} {request.surname.Trim()}",
                request.licenseNumber,
                phoneNumberVo,
                request.currentLatitude,
                request.currentLongitude
            );

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)null);
        _repoMock.Setup(repo => repo.GetByLicenseNumberAsync(request.licenseNumber, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)existingDriver);
        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, request.name, request.surname, request.licenseNumber, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
    [TestMethod]
    public async Task ShouldFailToCreateDriverProfileIfPhoneNumberCreationThrowsException_CreateDriverAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            licenseNumber = "AB149003LL",
            countryCode = "",
            phoneNumber = "",
            currentLatitude = 166273085.0,
            currentLongitude = 19999340053.0
        };
        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)null);
        _repoMock.Setup(repo => repo.GetByLicenseNumberAsync(request.licenseNumber, CancellationToken.None))
           .ReturnsAsync((DriverProfile?)null);
        // Act
        var result = await _driverProfileService.CreateDriverAsync(request.userId, request.name, request.surname, request.licenseNumber, request.countryCode, request.phoneNumber, request.currentLatitude, request.currentLongitude);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
}
