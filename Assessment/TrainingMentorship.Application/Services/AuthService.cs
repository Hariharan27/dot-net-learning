using System;
using Microsoft.Extensions.Configuration;
using TrainingMentorship.Application.DTOs.Auth;
using TrainingMentorship.Application.interfaces;
using BCrypt.Net;
using TrainingMentorship.Domain.Entities;
using System.Security.Claims;
using System.Text;
using TrainingMentorship.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace TrainingMentorship.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }


    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        string token = GenerateJwtToken(user);

        return new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            FullName = user.FullName,
            Role = user.Role.ToString()
        };
    }

    public async Task<int> RegisterAsync(RegisterRequestDto dto)
    {
        var exists = await _userRepo.GetByEmailAsync(dto.Email);
        if (exists != null)
            return -1;

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow
        };

        return await _userRepo.CreateAsync(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("fullName", user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<bool> UpdatePasswordAsync(UpdatePasswordDto dto)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        return await _userRepo.UpdatePasswordAsync(dto.UserId, hash);
    }



}

