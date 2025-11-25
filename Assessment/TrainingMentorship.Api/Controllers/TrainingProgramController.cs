using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrainingMentorship.Application.DTOs.Program;
using TrainingMentorship.Application.Services;
using TrainingMentorship.Application.DTOs.TrainingProgram;
using TrainingMentorship.Application.DTOs.Task;

namespace TrainingMentorship.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingProgramController : ControllerBase
{
    private readonly TrainingProgramService _service;

    public TrainingProgramController(TrainingProgramService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateProgramDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { ProgramId = id });
    }

    [HttpGet()]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);

    }


    [HttpGet("mentors/{mentorId}/programs")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetAllProgramsByMentorId(int mentorId)
    {
        var list = await _service.GetAllProgramByMentorIdAsync(mentorId);
        return Ok(list);
    }


    // GET PROGRAM DETAILS
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetDetails(int id)
    {
        var result = await _service.GetDetailsAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpGet("{programId:int}/mentor/{mentorId:int}")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetProgramDetailsForMentor(int programId, int mentorId)
    {
        var result = await _service.GetDetailsForMentorAsync(programId, mentorId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }



    [HttpGet("{programId}/mentor/{mentorId}/trainees")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetTraineesForMentor(int programId, int mentorId)
    {
        var list = await _service.GetTraineesForMentorAsync(programId, mentorId);
        return Ok(list);
    }


    [HttpGet("{programId}/trainee/{traineeId}/tasks")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetTasksForTraineeInProgram(int programId, int traineeId)
    {
        var tasks = await _service.GetTasksForTraineeInProgram(programId, traineeId);
        return Ok(tasks);
    }



    [HttpGet("{programId}/available-mentors")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAvailableMentors(int programId)
    {
        var list = await _service.GetAvailableMentorsAsync(programId);
        return Ok(list);
    }


    [HttpPost("assign-mentor")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignMentor(CreateProgramMentorDto dto)
    {
        var success = await _service.AssignMentorAsync(dto);

        if (!success)
            return BadRequest(new { message = "Mentor already assigned to this program." });

        return Ok(new { message = "Mentor assigned successfully." });
    }

    [HttpPost("assign-trainee")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> AssignTrainee([FromBody] AssignTraineeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid data" });

        // 👉 IMPORTANT: Call the new correct method
        var success = await _service.AssignTraineeWithMentorAsync(dto);

        if (!success)
            return BadRequest(new { message = "Mapping or assignment failed" });

        return Ok(new { message = "Trainee assigned successfully" });
    }


    [HttpGet("program-mentors/{programId}")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetMentorsForProgram(int programId)
    {
        var mentors = await _service.GetMentorsForProgramAsync(programId);

        return Ok(mentors.Select(m => new
        {
            m.MentorId,
            m.Mentor.FullName,
            m.Mentor.Email
        }));
    }



    [HttpGet("available-trainees/{programId}")]
    [Authorize(Roles = "Admin,Mentor")]
    public async Task<IActionResult> GetAvailableTrainees(int programId)
    {
        var trainees = await _service.GetAvailableTraineesAsync(programId);

        return Ok(trainees.Select(t => new
        {
            t.Id,
            t.FullName,
            t.Email
        }));
    }


    [HttpGet("trainee/{traineeId:int}")]
    [Authorize(Roles = "Admin,Trainee")]
    public async Task<IActionResult> GetProgramsForTrainee(int traineeId)
    {
        try
        {
            var result = await _service.GetProgramsForTraineeAsync(traineeId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message + " | " + ex.InnerException?.Message);
        }
    }


    [HttpGet("{programId:int}/trainee/{traineeId:int}")]
    [Authorize(Roles = "Admin,Trainee")]
    public async Task<IActionResult> GetProgramDetailsForTrainee(int programId, int traineeId)
    {
        var result = await _service.GetDetailsForTraineeAsync(programId, traineeId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }



    [HttpPost("{programId:int}/tasks")]
    public async Task<IActionResult> CreateTask(int programId, [FromBody] CreateTaskDto dto)
    {
        dto.ProgramId = programId;

        var success = await _service.CreateTaskAsync(dto);

        if (!success)
            return BadRequest("Failed to create task.");

        return Ok(new { Message = "Task created successfully." });
    }


    [HttpPut("task/update-status")]
    public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusDto dto)
    {
        var result = await _service.UpdateTaskStatusAsync(dto);

        if (!result)
            return BadRequest("Unable to update task status.");

        return Ok();
    }



}


