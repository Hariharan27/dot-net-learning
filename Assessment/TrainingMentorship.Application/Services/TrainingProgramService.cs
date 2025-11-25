using System;
using System.Net.Http;
using TrainingMentorship.Application.DTOs.Auth;
using TrainingMentorship.Application.DTOs.Program;
using TrainingMentorship.Application.DTOs.Task;
using TrainingMentorship.Application.DTOs.TrainingProgram;
using TrainingMentorship.Application.interfaces;
using TrainingMentorship.Domain.Entities;
using TrainingMentorship.Domain.Enums;

namespace TrainingMentorship.Application.Services;


public class TrainingProgramService
{
    private readonly ITrainingProgramRepository _repo;

    public TrainingProgramService(ITrainingProgramRepository repo)
    {
        _repo = repo;
    }

    // CREATE PROGRAM
    public async Task<int> CreateAsync(CreateProgramDto dto)
    {
        var program = new TrainingProgram
        {
            Title = dto.Title,
            Description = dto.Description
        };

        return await _repo.CreateAsync(program);
    }

    // LIST ALL PROGRAMS
    public async Task<List<TrainingProgramListDto>> GetAllAsync()
    {

        var programs = await _repo.GetAllAsync();

        return programs.Select(x => new TrainingProgramListDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description
        }).ToList();

    }

    // GET DETAILS FOR PROGRAM PAGE
    public async Task<TrainingProgramDetailsDto?> GetDetailsAsync(int id)
    {
        var program = await _repo.GetByIdAsync(id);

        if (program == null)
            return null;

        return new TrainingProgramDetailsDto
        {
            Id = program.Id,
            Title = program.Title,
            Description = program.Description,

            MentorCount = program.Mentors.Count,
            TraineeCount = program.Trainees.Count,
            TaskCount = program.Tasks.Count,

            Mentors = program.Mentors.Select(m => new ProgramMentorDto
            {
                MentorId = m.MentorId,
                FullName = m.Mentor.FullName,
                Email = m.Mentor.Email
            }).ToList()
        };
    }


    // GET DETAILS FOR PROGRAM PAGE
    public async Task<TrainingProgramDetailsDto?> GetDetailsForMentorAsync(int programId, int mentorId)
    {
        var program = await _repo.GetProgramDetailsForMentorAsync(programId, mentorId);

        if (program == null)
            return null;

        return new TrainingProgramDetailsDto
        {
            Id = program.Id,
            Title = program.Title,
            Description = program.Description,

            // mentor-specific counts
            MentorCount = 1, // logged-in mentor
            TraineeCount = program.MentorTraineeLinks.Count,
            TaskCount = program.Tasks.Count,

            // MENTOR VIEW → trainees only
            Trainees = program.MentorTraineeLinks.Select(t => new ProgramTraineeDto
            {
                TraineeId = t.TraineeId,
                FullName = t.Trainee.FullName,
                Email = t.Trainee.Email,
            }).ToList()
        };
    }



    public async Task<List<ProgramTraineeDto>> GetTraineesForMentorAsync(int programId, int mentorId)
    {
        return await _repo.GetTraineesForMentorAsync(programId, mentorId);
    }


    public async Task<List<ProgramTraineeTaskDto>> GetTasksForTraineeInProgram(int programId, int traineeId)
    {
        return await _repo.GetTasksForTraineeInProgram(programId, traineeId);
    }


    public async Task<List<UserDto>> GetAvailableMentorsAsync(int programId)
    {
        var mentors = await _repo.GetAvailableMentorsAsync(programId);

        return mentors.Select(m => new UserDto
        {
            Id = m.Id,
            FullName = m.FullName,
            Email = m.Email
        }).ToList();
    }


    public async Task<bool> AssignMentorAsync(CreateProgramMentorDto dto)
    {
        return await _repo.AssignMentorAsync(dto.ProgramId, dto.MentorId);
    }


    public async Task<bool> AssignTraineeWithMentorAsync(AssignTraineeDto dto)
    {
        return await _repo.AssignTraineeWithMentorAsync(dto);
    }

    public async Task<List<TrainingProgramMentor>> GetMentorsForProgramAsync(int programId)
    {
        return await _repo.GetMentorsForProgramAsync(programId);
    }


    public async Task<List<User>> GetAvailableTraineesAsync(int programId)
    {
        return await _repo.GetAvailableTraineesAsync(programId);
    }

    public async Task<List<TrainingProgramListDto>> GetAllProgramByMentorIdAsync(int mentorId)
    {
        var programs = await _repo.GetProgramsByMentorIdAsync(mentorId);

        return programs.Select(x => new TrainingProgramListDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description
        }).ToList();
    }


    public async Task<List<TrainingProgramListDto>> GetProgramsForTraineeAsync(int traineeId)
    {
        var programs = await _repo.GetProgramsForTraineeAsync(traineeId);

        return programs.Select(p => new TrainingProgramListDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description
        }).ToList();
    }


    public async Task<TrainingProgramDetailsDto?> GetDetailsForTraineeAsync(int programId, int traineeId)
    {
        var program = await _repo.GetByIdAsync(programId);
        // NOTE: create GetByIdBasicAsync to load only Program fields WITHOUT mentors/trainees

        if (program == null)
            return null;

        // 🔥 Load tasks using your method
        var tasks = await _repo.GetTasksForTraineeInProgram(programId, traineeId);

        return new TrainingProgramDetailsDto
        {
            Id = program.Id,
            Title = program.Title,
            Description = program.Description,
            MentorCount = 0,
            TraineeCount = 1,
            TaskCount = tasks.Count,

            Tasks = tasks.ToList()
        };
    }

    public async Task<bool> CreateTaskAsync(CreateTaskDto dto)
    {
        // Validate that program exists
        var program = await _repo.GetByIdAsync(dto.ProgramId);
        if (program == null)
            return false;

        // Call repo method to create the task and assign to trainees
        return await _repo.CreateTaskAsync(dto);
    }


    public async Task<bool> UpdateTaskStatusAsync(UpdateTaskStatusDto dto)
    {
        // Call repo method to create the task and assign to trainees
        return await _repo.UpdateTaskStatusAsync(dto);
    }


}


