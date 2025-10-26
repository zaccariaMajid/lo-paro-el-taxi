using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElTaxi.UnitTests;

[TestClass]
public class PasswordServiceTests
{
    protected IPasswordService _passwordService = null!;
    [TestInitialize]
    public void Setup()
    {
        _passwordService = new ElTaxi.Application.Services.PasswordService();
    }

    [TestMethod]
    public void ShouldCorrectlyHashPassword_HashPassword()
    {
        var password = "my_secret_password";
        var hashedPassword = _passwordService.HashPassword(password);

        Assert.IsNotNull(hashedPassword);
        Assert.AreNotEqual(password, hashedPassword);
    }

    [TestMethod]
    public void ShouldCorrectlyVerifyPassword_VerifyPassword()
    {
        var password = "my_secret_password";
        var hashedPassword = _passwordService.HashPassword(password);

        var isVerified = _passwordService.VerifyPassword(hashedPassword, password);
        Assert.IsTrue(isVerified);
    }

    [TestMethod]
    public void ShouldFailToVerifyIncorrectPassword_VerifyPassword()
    {
        var password = "my_secret_password";
        var wrongPassword = "my_wrong_password";
        var hashedPassword = _passwordService.HashPassword(password);
        var isVerified = _passwordService.VerifyPassword(hashedPassword, wrongPassword);
        Assert.IsFalse(isVerified);
    }
}
