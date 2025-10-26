using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;

namespace ElTaxi.Application.Services;

public class PasswordService : IPasswordService
{
    // number of rounds for hashing
    private readonly int _workFacotor;
    public PasswordService(int WorkFactor = 12)
    {
        _workFacotor = WorkFactor;
    }
    public string HashPassword(string input)
        => BCrypt.Net.BCrypt.HashPassword(input, _workFacotor);

    public bool VerifyPassword(string hashedString, string input)
        => BCrypt.Net.BCrypt.Verify(input, hashedString);
}
