using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Aggregates;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Application.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(IUserRepository userRepository, IPasswordService passwordService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
    }
    public Task<Result<AuthResponse>> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<AuthResponse>> RegisterAsync(string email, string password, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<AuthResponse>.Fail("Email must be provided.");

        if (string.IsNullOrWhiteSpace(password))
            return Result<AuthResponse>.Fail("Password must be provided.");

        if (!Enum.IsDefined(typeof(UserRole), role))
            return Result<AuthResponse>.Fail("Invalid role specified.");
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
            return Result<AuthResponse>.Fail("Email is already registered.");

        var emailVo = Email.Create(email);

        var hashedPassword = _passwordService.HashPassword(password);

        var user = User.Create(emailVo, hashedPassword, role);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result<AuthResponse>.Success(new AuthResponse(user.Id, user.Email.Value));
    }
}

public record AuthResponse(Guid UserId, string Email);