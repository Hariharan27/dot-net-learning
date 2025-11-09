using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebAPI.Models;

public class RegisterUser
{
    [Required]
    [StringLength(50)]
    public string? Username { get; set; }

    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }

    [Required]
    [MinLength(8)]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "PassWord does not match")]
    public string? ConfirmPassword { get; set; }


    [Validators.NoBadWords]
    public string? Comment { get; set; }

}


