using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AuthLoginResponse>> LoginAsync(string email, string password, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<AuthLoginResponse>.Fail("Email must be provided.");
        if (string.IsNullOrWhiteSpace(password))
            return Result<AuthLoginResponse>.Fail("Password must be provided.");
        
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            return Result<AuthLoginResponse>.Fail("Invalid email or password.");

        if(user.Status == UserStatus.Inactive)
            return Result<AuthLoginResponse>.Fail("User account is inactive.");

        if (user.Status == UserStatus.Banned)
            return Result<AuthLoginResponse>.Fail("User account is banned.");

        if (!_passwordService.VerifyPassword(user.PasswordHash, password))
            return Result<AuthLoginResponse>.Fail("Invalid email or password.");

        string token = _tokenService.GenerateToken(user.Id, user.Email.Value, user.Role.ToString()).Value;

        if(string.IsNullOrWhiteSpace(token))
            return Result<AuthLoginResponse>.Fail("Error while generating token");
            
        return Result<AuthLoginResponse>.Success(new AuthLoginResponse(token));
    }

    public async Task<Result<AuthRegisterResponse>> RegisterAsync(string email, string password, UserRole role, CancellationToken ct = default)
    {
        var validationResult = await this.validateRegisterRequest(email, password, role);
        if (!validationResult.IsSuccess)
            return Result<AuthRegisterResponse>.Fail(validationResult.Error!);

        var emailVo = Email.Create(email);

        var hashedPassword = _passwordService.HashPassword(password);

        var user = User.Create(emailVo, hashedPassword, role);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result<AuthRegisterResponse>.Success(new AuthRegisterResponse(user.Id, user.Email.Value));
    }

    private async Task<Result<bool>> validateRegisterRequest(string email, string password, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<bool>.Fail("Email must be provided.");

        if (string.IsNullOrWhiteSpace(password))
            return Result<bool>.Fail("Password must be provided.");

        if (!Enum.IsDefined(typeof(UserRole), role))
            return Result<bool>.Fail("Invalid role specified.");

        if (await _userRepository.GetByEmailAsync(email) is not null)
            return Result<bool>.Fail("Email is already registered.");

        return Result<bool>.Success(true);
    }
}

public record AuthRegisterResponse(Guid userId, string email);
public record AuthLoginResponse(string token);