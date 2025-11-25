using System;
using TrainingMentorship.Domain.Enums;

namespace TrainingMentorship.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public UserRole Role { get; set; }
}

