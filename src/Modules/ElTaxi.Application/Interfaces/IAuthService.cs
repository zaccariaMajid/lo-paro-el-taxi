using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.Application.Interfaces;

public interface IAuthService
{
    Task<string> Register(string email, string password, string role);
    Task<string> Login(string email, string password);
}
