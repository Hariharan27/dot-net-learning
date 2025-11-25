using System;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Services;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers;


[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
	private readonly IStudentService _studentService;

	public StudentController(IStudentService studentService)
	{
		_studentService = studentService;
	}


	[HttpGet("{id}")]
	public async Task<IActionResult> GetStudentById(int id)
	{
		var student = await _studentService.GetStudentById(id);
		if(student == null)
		{
			return NotFound();
		}
		return Ok(student);
	}

	[HttpPost("save")]
	public async Task<IActionResult> AddStudent([FromBody] Student student)
	{
	 	var result =  await _studentService.AddStudent(student);
		return Ok(result);
	}

	[HttpDelete("{id}/remove")]
	public async Task<IActionResult> RemoveStudent(int id)
	{
		var result = await _studentService.RemoveStudent(id);
		return Ok(result);
	}

	[HttpGet("by-major")]
	public async Task<IActionResult> GetStudentByMajorStatus([FromQuery] int isMajor)
	{
		if (isMajor == 1)
		{
			var students = await _studentService.GetMajorStudents();
            return Ok(students);
        }
		else
		{
			var students = await _studentService.GetMinorStudents();
            return Ok(students);
        }
		
	}


    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var studentList = await _studentService.GetAllStudents();
        return Ok(studentList);
    }

}

