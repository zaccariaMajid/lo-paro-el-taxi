using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElTaxi.UnitTests;

[TestClass]
public class AuthServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordService> _passwordServiceMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IConfiguration> _configrationMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [TestMethod]
    public async Task ShouldCorrectlyRegisterUser_RegisterAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
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
        Assert.AreEqual(request.Email, result.Value.email);
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task ShouldFailToRegisterUser_IfEmailAlreadyExists_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        // arrange an existing user to simulate "email already exists"
        var userEmail = Email.Create(request.Email);
        var existingUser = User.Create(userEmail, "existing_hashed_password", (UserRole)request.Role);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(existingUser);
        _passwordServiceMock.Setup(service => service.HashPassword(request.Password))
            .Returns("hashed_password");

        // Act
        var result = await authService.RegisterAsync(request.Email, request.Password, (UserRole)request.Role);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToRegisterUser_IfRoleDoesNotExist_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
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
    public async Task ShouldFailToRegisterUser_IfEmailIsWrongFormat_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
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
    public async Task ShouldFailToRegisterUser_IfPasswordIsEmpty_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
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
    [TestMethod]
    public async Task ShouldFailToRegisterUser_IfEmailIsEmpty_RegisterAsync()
    {
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "",
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

    [TestMethod]
    public async Task ShouldCorrectlyLoginUser_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        var userId = Guid.NewGuid();

        // arrange an existing user to simulate "email already exists"
        var userEmail = Email.Create(request.Email);
        var existingUser = User.Create(userEmail, "existing_hashed_password", (UserRole)request.Role);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(existingUser);
        _passwordServiceMock.Setup(service => service.VerifyPassword(It.IsAny<string>(), request.Password))
            .Returns(true);
        _tokenServiceMock.Setup(s => s.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Result<string>.Success("TOKEN"));

        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
    }


    [TestMethod]
    public async Task ShouldFailToLoginUserIfUserNotExists_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        var userId = Guid.NewGuid();

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User?)null);
        _passwordServiceMock.Setup(service => service.VerifyPassword(It.IsAny<string>(), request.Password))
            .Returns(true);
        _tokenServiceMock.Setup(s => s.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Result<string>.Success("TOKEN"));

        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToLoginUserIfPasswordIsIncorrect_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        var userId = Guid.NewGuid();

        // arrange an existing user to simulate "email already exists"
        var userEmail = Email.Create(request.Email);
        var existingUser = User.Create(userEmail, "existing_hashed_password", (UserRole)request.Role);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(existingUser);
        _passwordServiceMock.Setup(service => service.VerifyPassword(It.IsAny<string>(), request.Password))
            .Returns(false);
        _tokenServiceMock.Setup(s => s.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Result<string>.Success("TOKEN"));

        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToLoginUserIfUserIsInactive_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };

        // arrange an existing user to simulate "email already exists"
        var userEmail = Email.Create(request.Email);
        var existingUser = User.Create(userEmail, "existing_hashed_password", (UserRole)request.Role);

        existingUser.ChangeStatus(UserStatus.Inactive);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(existingUser);

        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToLoginUserIfUserIsBanned_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };

        // arrange an existing user to simulate "email already exists"
        var userEmail = Email.Create(request.Email);
        var existingUser = User.Create(userEmail, "existing_hashed_password", (UserRole)request.Role);

        existingUser.ChangeStatus(UserStatus.Banned);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(existingUser);

        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToLoginUserIfEmailIsEmpty_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "",
            Password = "secure_password",
            Role = 2
        };
        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldFailToLoginUserIfPasswordIsEmpty_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "",
            Role = 2
        };
        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
    
    [TestMethod]
    public async Task ShouldFailToLoginUserIfTokenIsEmpty_LoginAsync()
    {
        // Arrange
        var authService = new AuthService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object, _unitOfWorkMock.Object);
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        var userId = Guid.NewGuid();

        // arrange an existing user to simulate "email already exists"
        var userEmail = Email.Create(request.Email);
        var existingUser = User.Create(userEmail, "existing_hashed_password", (UserRole)request.Role);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(existingUser);
        _passwordServiceMock.Setup(service => service.VerifyPassword(It.IsAny<string>(), request.Password))
            .Returns(true);
        _tokenServiceMock.Setup(s => s.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Result<string>.Success(""));

        // Act
        var result = await authService.LoginAsync(request.Email, request.Password);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
}
