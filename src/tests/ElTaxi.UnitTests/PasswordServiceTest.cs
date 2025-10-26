using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElTaxi.Application.Interfaces;

namespace ElTaxi.UnitTests;

[TestClass]
public class PasswordServiceTest
{
    protected IPasswordService _passwordService;

    [TestInitialize]
    public void Setup()
    {
        _passwordService = new PasswordService();
    }

    [TestMethod]
    public void ShouldCorrectlyHashPassword()
    {
        var password = "my_secret_password";
        var hashedPassword = _passwordService.HashPassword(password);

        Assert.IsNotNull(hashedPassword);
        Assert.AreNotEqual(password, hashedPassword);
    }
}
