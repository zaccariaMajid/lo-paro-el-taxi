using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
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
public class AuthServiceTest
{
    private readonly Mock<IPasswordService> _passwordServiceMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [TestMethod]
    public async Task ShouldCorrectlyRegisterUser_RegisterAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User?)null);
        _passwordServiceMock.Setup(service => service.HashPassword(request.Password))
            .Returns("hashed_password");

        // Act
        var result = await authService.RegisterAsync(request.Email, request.Password, (UserRole)request.Role);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(request.Email, result.Value.Email);
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task ShouldFailToRegisterUser_WhenEmailAlreadyExists_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 6
        };
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User?)null);
        _passwordServiceMock.Setup(service => service.HashPassword(request.Password))
            .Returns("hashed_password");

        // Act
        var result = await authService.RegisterAsync(request.Email, request.Password, (UserRole)request.Role);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToRegisterUser_WhenRoleDoesNotExist_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 4
        };
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User?)null);
        _passwordServiceMock.Setup(service => service.HashPassword(request.Password))
            .Returns("hashed_password");

        // Act
        var result = await authService.RegisterAsync(request.Email, request.Password, (UserRole)request.Role);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task ShouldFailToRegisterUser_WhenEmailIsWrongFormat_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45 gmail.com",
            Password = "secure_password",
            Role = 2
        };
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User?)null);
        _passwordServiceMock.Setup(service => service.HashPassword(request.Password))
            .Returns("hashed_password");

        // Act
        var result = await authService.RegisterAsync(request.Email, request.Password, (UserRole)request.Role);
    }


    [TestMethod]
    public async Task ShouldFailToRegisterUser_WhenPasswordIsEmpty_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "",
            Role = 2
        };
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User?)null);
        _passwordServiceMock.Setup(service => service.HashPassword(request.Password))
            .Returns("hashed_password");

        // Act
        var result = await authService.RegisterAsync(request.Email, request.Password, (UserRole)request.Role);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
}
