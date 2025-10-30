using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ElTaxi.BuildingBlocks.Application;

namespace ElTaxi.Application.Services;

public class TokenService : ITokenService
{
    protected readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Result<string> GenerateToken(Guid userId, string email, string role)
    {
        JwtSecurityToken token;
        try
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("my_super_long_secret_key_123456789"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);

            var claims = new[]
            {
                new System.Security.Claims.Claim("userId", userId.ToString()),
                new System.Security.Claims.Claim("email", email),
                new System.Security.Claims.Claim("role", role)
            };

            token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Issuer missing"),
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
        }
        catch (Exception ex)
        {
            return Result<string>.Fail(ex.Message);
        }

        return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }
}