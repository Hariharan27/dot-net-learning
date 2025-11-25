using System;
namespace TrainingMentorship.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public int UserId { get; set; }
    public string FullName { get; set; } = default!;
    public string Role { get; set; } = default!;

}

