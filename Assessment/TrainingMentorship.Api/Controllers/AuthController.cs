using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TrainingMentorship.Application.DTOs.Auth;
using TrainingMentorship.Application.Services;
using TrainingMentorship.Domain.Enums;

namespace TrainingMentorship.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{

    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // LOGIN ---------------------------------------------------------
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized(new { message = "Invalid email or password" });

        return Ok(result);
    }


    // REGISTER ------------------------------------------------------
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        var id = await _authService.RegisterAsync(dto);

        if (id == -1)
            return BadRequest(new { message = "Email already exists" });

        return Ok(new { userId = id, message = "User registered successfully" });
    }

    // Password Change ------------------------------------------------------
    [HttpPut("update-password")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDto dto)
    {
        var success = await _authService.UpdatePasswordAsync(dto);

        if (!success)
            return NotFound(new { message = "User not found" });

        return Ok(new { message = "Password updated successfully" });
    }


}

