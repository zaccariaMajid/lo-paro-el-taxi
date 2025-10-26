using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.Application.Interfaces;

public interface IPasswordService
{
    string HashPassword(string input);
    bool VerifyPassword(string hashedString, string input);
}
