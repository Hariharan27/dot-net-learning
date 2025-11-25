using System;
using TrainingMentorship.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using TrainingMentorship.Application.DTOs.Program;
using TrainingMentorship.MVC.Models;
using TrainingMentorship.Application.DTOs.TrainingProgram;
using System.Data;
using TrainingMentorship.Domain.Entities;
using TrainingMentorship.Application.DTOs.Task;

namespace TrainingMentorship.MVC.Controllers;

public class TrainingProgramController : Controller
{
    private readonly TrainingProgramApiService _api;

    private readonly TraineeTaskApiService _taskService;

    private readonly ProgramMentorApiService _programMentorApiService;

    public TrainingProgramController(TrainingProgramApiService api, TraineeTaskApiService taskService,
        ProgramMentorApiService programMentorApiService)
    {
        _api = api;
        _taskService = taskService;
        _programMentorApiService = programMentorApiService;
    }

    // ===================== INDEX =====================
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null)
            return RedirectToAction("Login", "Account");

        // Use REAL VARIABLES
        var role = HttpContext.Session.GetString("UserRole");
        int? userId = HttpContext.Session.GetInt32("UserId");

        // Pass to ViewBag ONLY FOR VIEW
        ViewBag.Role = role;
        ViewBag.UserId = userId;

        if (role == "Admin")
        {
            var programs = await _api.GetAllAsync(token);
            return View(programs);
        }
        else if (role == "Mentor" && userId != null)
        {
            var programs = await _api.GetAllProgramsByMentorIdAsync(token, userId.Value);
            return View(programs);
        }
        else if (role == "Trainee" && userId != null)
        {
            var programs = await _api.GetProgramsForTraineeAsync(token, userId.Value);
            return View(programs);
        }

        return RedirectToAction("Login", "Account");
    }




    // ===================== CREATE (GET) =====================
    public IActionResult Create()
    {
        return View();
    }

    // ===================== CREATE (POST) =====================
    [HttpPost]
    public async Task<IActionResult> Create(CreateProgramDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null)
            return RedirectToAction("Login", "Account");

        var id = await _api.CreateAsync(model, token);

        if (id == null)
        {
            ViewBag.Error = "Unable to create program.";
            return View(model);
        }

        TempData["Success"] = "Program created successfully!";
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Details(int id)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null)
            return RedirectToAction("Login", "Account");

        var role = HttpContext.Session.GetString("UserRole");
        var userId = HttpContext.Session.GetInt32("UserId");

        ViewBag.Role = role;
        ViewBag.UserId = userId;

        TrainingProgramDetailsDto? program = null;

        if (role == "Admin")
        {
            // ADMIN → Show full program details including mentor list
            program = await _api.GetDetailsAsync (id, token);
        }
        else if (role == "Mentor" && userId.HasValue)
        {
            // MENTOR → Show program details filtered to THEIR trainees only
            program = await _api.GetProgramDetailsForMentorAsync(id, userId.Value, token);
        }
        else if(role == "Trainee" && userId.HasValue)
        {
            program = await _api.GetProgramDetailsForTraineeAsync(token, id, userId.Value);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }

        if (program == null)
            return NotFound();

        return View(program);
    }



    public async Task<IActionResult> MentorTrainees(int programId, int mentorId)
    {
        var token = HttpContext.Session.GetString("JwtToken")!;

        var trainees = await _api.GetTraineesForMentorAsync(programId, mentorId, token);

        ViewBag.ProgramId = programId;

        return View(trainees);
    }


    public async Task<IActionResult> TraineeTasks(int programId, int traineeId)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null) return RedirectToAction("Login", "Account");

        var tasks = await _taskService.GetTasksAsync(programId, traineeId, token);

        ViewBag.ProgramId = programId;
        ViewBag.TraineeId = traineeId;

        return View(tasks);
    }


    public async Task<IActionResult> AssignMentor(int programId)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null) return RedirectToAction("Login", "Account");

        var mentors = await _api.GetAvailableMentorsAsync(programId, token);

        ViewBag.ProgramId = programId;
        return View(mentors);
    }


    [HttpPost]
    public async Task<IActionResult> AssignMentorPost(AssignMentorViewModel model)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null) return RedirectToAction("Login", "Account");

        await _programMentorApiService.AssignMentorAsync(model.ProgramId, model.MentorId, token);

        TempData["Success"] = "Mentor assigned successfully!";
        return RedirectToAction("Details", new { id = model.ProgramId });
    }



    [HttpPost]
    public async Task<IActionResult> AssignMentor(int programId, int mentorId)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null) return RedirectToAction("Login", "Account");

        var result = await _programMentorApiService.AssignMentorAsync(programId, mentorId, token);

        if (!result)
        {
            TempData["Error"] = "Mentor already assigned!";
            return RedirectToAction("Details", new { id = programId });
        }

        TempData["Success"] = "Mentor assigned successfully!";
        return RedirectToAction("Details", new { id = programId });
    }



    [HttpGet]
    public async Task<IActionResult> AssignTrainee(int programId)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null) return RedirectToAction("Login", "Account");

        // 1️⃣ Load trainees not in this program
        var availableTrainees = await _programMentorApiService.GetAvailableTrainees(programId, token);

        // 2️⃣ Load mentors already assigned to this program
        var mentors = await _programMentorApiService.GetMentorsForProgramAsync(programId, token);

        var vm = new AssignTraineeViewModel
        {
            ProgramId = programId,
            AvailableTrainees = availableTrainees,
            Mentors = mentors
        };

        return View(vm);
    }



    [HttpPost]
    public async Task<IActionResult> AssignTrainee(AssignTraineeViewModel model)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null)
            return RedirectToAction("Login", "Account");

        // =============== VALIDATION ==================
        if (model.MentorIds == null || !model.MentorIds.Any())
        {
            ModelState.AddModelError("MentorIds", "Please select at least one mentor.");
        }

        if (model.TraineeId == null)
        {
            ModelState.AddModelError("TraineeIds", "Please select at least one trainee.");
        }
        // ===============================================

        if (!ModelState.IsValid)
        {
            model.AvailableTrainees = await _programMentorApiService.GetAvailableTrainees(model.ProgramId, token);
            model.Mentors = await _programMentorApiService.GetMentorsForProgramAsync(model.ProgramId, token);
            return View(model);
        }

        var success = await _programMentorApiService.AssignTraineeAsync(model, token);

        if (!success)
        {
            TempData["Error"] = "Unable to assign trainee. Try again.";
            return RedirectToAction("AssignTrainee", new { programId = model.ProgramId });
        }

        TempData["Success"] = "Trainee assigned successfully!";
        return RedirectToAction("Details", new { id = model.ProgramId });
    }



    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskViewModel model)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (token == null)
            return RedirectToAction("Login", "Account");

        var dto = new CreateTaskDto
        {
            ProgramId = model.ProgramId,
            Title = model.Title,
            Description = model.Description
        };

        var success = await _api.CreateTaskAsync(token, dto);

        if (!success)
            TempData["Error"] = "Failed to create task.";

        return RedirectToAction("Details", new { id = model.ProgramId });
    }



    [HttpPost]
    public async Task<IActionResult> UpdateTaskStatus(UpdateTaskStatusViewModel model)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        var traineeId = HttpContext.Session.GetInt32("UserId");

        if (token == null || traineeId == null)
            return RedirectToAction("Login", "Account");

        var dto = new UpdateTaskStatusDto
        {
            TaskItemId = model.TaskItemId,
            TraineeId = traineeId.Value,
            Status = model.Status
        };

        var success = await _api.UpdateTaskStatusAsync(token, dto);

        if (!success)
            TempData["Error"] = "Failed to update task status.";

        return RedirectToAction("Details", new { id = model.ProgramId });
    }


}



