using System;
namespace TrainingMentorship.Application.DTOs.Auth;

public class UpdatePasswordDto
{
    public int UserId { get; set; }
    public string NewPassword { get; set; } = default!;

}

