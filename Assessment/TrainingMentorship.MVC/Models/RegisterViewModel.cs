using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingMentorship.MVC.Models;

public class RegisterViewModel
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public int Role { get; set; }   // 1=Admin, 2=Mentor, 3=Trainee
}

