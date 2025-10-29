using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElTaxi.UnitTests;

[TestClass]
public class TokenServiceTest
{
    protected TokenService _tokenService = null!;
    protected IConfiguration _config = null!;

    [TestInitialize]
    public void Setup()
    {
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(conf => conf["Jwt:Issuer"]).Returns("https://myapi.example.com");
        mockConfig.Setup(conf => conf["Jwt:Audience"]).Returns("ME");

        _config = mockConfig.Object;

        _tokenService = new TokenService(_config);
    }

    [TestMethod]
    public void ShouldGenerateToken_GenerateToken()
    {
        // Arrange
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        var userId = Guid.NewGuid();

        // Act
        var result = _tokenService.GenerateToken(userId, request.Email, ((UserRole)request.Role).ToString());

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }
    [TestMethod]
    public void ShouldFailToGenerateTokenIfIssuerDoesNotExist_GenerateToken()
    {
        var mockConfig2 = new Mock<IConfiguration>();
        mockConfig2.Setup(conf => conf["Jwt:Audience"]).Returns("ME");

        var _config2 = mockConfig2.Object;

        var _tokenService2 = new TokenService(_config2);

        // Arrange
        var request = new
        {
            Email = "zaccariam45@gmail.com",
            Password = "secure_password",
            Role = 2
        };
        var userId = Guid.NewGuid();

        // Act
        var result = _tokenService2.GenerateToken(userId, request.Email, ((UserRole)request.Role).ToString());

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Issuer missing", result.Error);
    }
}
