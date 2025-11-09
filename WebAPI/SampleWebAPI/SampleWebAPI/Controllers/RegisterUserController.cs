using System;
using Microsoft.AspNetCore.Mvc;
namespace SampleWebAPI.Controllers;
using SampleWebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class RegisterUserController : ControllerBase
{
	private readonly ILogger<RegisterUserController> _logger;

	public RegisterUserController(ILogger<RegisterUserController> logger)
	{
		_logger = logger;
	}

	[HttpPost]
	public IActionResult AddRegisterUser([FromBody] RegisterUser user)
	{
		var result = new {
			Message = $"User Registered Scuccessfully with email: {user.EmailAddress}"
		};
		return Ok(result);
	}


	[HttpGet("/info")]
	public IActionResult GetUserInfomration()
	{
		var result = new
		{
			IsUserApi = true
		};
		return Ok(result);
	}

}

