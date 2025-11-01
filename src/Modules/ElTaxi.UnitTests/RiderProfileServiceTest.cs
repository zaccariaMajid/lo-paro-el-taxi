using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElTaxi.UnitTests;

[TestClass]
public class RiderProfileServiceTest
{
    private readonly Mock<IRiderProfileRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private RiderProfileService _riderProfileService = null!;

    [TestInitialize]
    public void Setup()
    {
        _riderProfileService = new RiderProfileService(_repoMock.Object, _unitOfWorkMock.Object);
    }

    [TestMethod]
    public async Task ShouldCorrectlyCreateRiderProfile_CreateRiderAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            countryCode = "39",
            phoneNumber = "3333333333",
            preferredPaymentType = PaymentTypes.CreditCard
        };

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((RiderProfile?)null);
        // Act
        var result = await _riderProfileService.CreateRiderAsync(request.userId, request.name, request.surname, request.countryCode, request.phoneNumber, request.preferredPaymentType);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateRiderProfileIfNameIsNull_CreateRiderAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "",
            surname = "Majid",
            countryCode = "39",
            phoneNumber = "3333333333",
            preferredPaymentType = PaymentTypes.CreditCard
        };

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((RiderProfile?)null);
        // Act
        var result = await _riderProfileService.CreateRiderAsync(request.userId, request.name, request.surname, request.countryCode, request.phoneNumber, request.preferredPaymentType);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateRiderProfileIfSurnameIsNull_CreateRiderAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "",
            countryCode = "39",
            phoneNumber = "3333333333",
            preferredPaymentType = PaymentTypes.CreditCard
        };

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((RiderProfile?)null);
        // Act
        var result = await _riderProfileService.CreateRiderAsync(request.userId, request.name, request.surname, request.countryCode, request.phoneNumber, request.preferredPaymentType);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
    [TestMethod]
    public async Task ShouldFailToCreateRiderProfileIfPreferredPaymentTypeIsNotDefinedInEnum_CreateRiderAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            countryCode = "39",
            phoneNumber = "3333333333",
            preferredPaymentType = 7
        };

        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((RiderProfile?)null);
        // Act
        var result = await _riderProfileService.CreateRiderAsync(request.userId, request.name, request.surname, request.countryCode, request.phoneNumber, (PaymentTypes)request.preferredPaymentType);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateRiderProfileIfUserIsExisting_CreateRiderAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            countryCode = "39",
            phoneNumber = "3333333333",
            preferredPaymentType = PaymentTypes.CreditCard
        };
        var phoneNumberVo = PhoneNumber.Create(request.countryCode, request.phoneNumber);
        var existingUser = RiderProfile.Create(request.userId, $"{request.name} {request.surname}", phoneNumberVo, request.preferredPaymentType);
        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((RiderProfile?)existingUser);
        // Act
        var result = await _riderProfileService.CreateRiderAsync(request.userId, request.name, request.surname, request.countryCode, request.phoneNumber, request.preferredPaymentType);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToCreateRiderProfileIfhoneNumberCreationThrowsExceptio_CreateRiderAsync()
    {
        // Arrange
        var request = new
        {
            userId = Guid.NewGuid(),
            name = "Zaccaria",
            surname = "Majid",
            countryCode = "",
            phoneNumber = "",
            preferredPaymentType = PaymentTypes.CreditCard
        };
        _repoMock.Setup(repo => repo.GetByUserIdAsync(request.userId, CancellationToken.None))
           .ReturnsAsync((RiderProfile?)null);
        // Act
        var result = await _riderProfileService.CreateRiderAsync(request.userId, request.name, request.surname, request.countryCode, request.phoneNumber, request.preferredPaymentType);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
}
