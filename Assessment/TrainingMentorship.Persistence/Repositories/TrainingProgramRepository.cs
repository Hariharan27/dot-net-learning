using Microsoft.EntityFrameworkCore;
using TrainingMentorship.Domain.Entities;
using TrainingMentorship.Application.interfaces;
using TrainingMentorship.Persistence.context;
using TrainingMentorship.Domain.Enums;
using TrainingMentorship.Application.DTOs.TrainingProgram;
using TrainingMentorship.Application.DTOs.Task;

namespace TrainingMentorship.Persistence.Repositories;


public class TrainingProgramRepository : ITrainingProgramRepository
{
    private readonly ApplicationDbContext _db;

    public TrainingProgramRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    // CREATE PROGRAM
    public async Task<int> CreateAsync(TrainingProgram program)
    {
        _db.TrainingPrograms.Add(program);
        await _db.SaveChangesAsync();
        return program.Id;
    }

    // GET ALL PROGRAMS
    public async Task<List<TrainingProgram>> GetAllAsync()
    {
        return await _db.TrainingPrograms
            .AsNoTracking()
            .ToListAsync();
    }

    // GET PROGRAM DETAILS (Mentors + Trainees + Tasks)
    public async Task<TrainingProgram?> GetByIdAsync(int id)
    {
        return await _db.TrainingPrograms
            .Include(x => x.Mentors)                // TrainingProgramMentor
                .ThenInclude(m => m.Mentor)         // actual User
            .Include(x => x.Trainees)               // TraineePrograms
                .ThenInclude(t => t.Trainee)
            .Include(x => x.Tasks)                  // Tasks under program
            .Include(x => x.Schedules)              // Program schedule
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<TrainingProgram?> GetProgramDetailsForMentorAsync(int programId, int mentorId)
    {
        return await _db.TrainingPrograms
            .Where(p => p.Id == programId)

            // Include tasks and schedule normally
            .Include(p => p.Tasks)
            .Include(p => p.Schedules)

            // Include ONLY mapping rows where this mentor is the owner
            .Include(p => p.MentorTraineeLinks
                .Where(m => m.MentorId == mentorId))
                    .ThenInclude(m => m.Trainee)

            .AsNoTracking()
            .FirstOrDefaultAsync();
    }




    // UPDATE PROGRAM
    public async Task<bool> UpdateAsync(TrainingProgram program)
    {
        _db.TrainingPrograms.Update(program);
        return await _db.SaveChangesAsync() > 0;
    }


    public async Task<List<ProgramTraineeDto>> GetTraineesForMentorAsync(int programId, int mentorId)
    {
        return await _db.ProgramMentorTrainees
            .Where(x => x.ProgramId == programId && x.MentorId == mentorId)
            .Include(x => x.Trainee)
            .Select(x => new ProgramTraineeDto
            {
                TraineeId = x.TraineeId,
                FullName = x.Trainee.FullName,
                Email = x.Trainee.Email
            })
            .ToListAsync();
    }


    public async Task<List<ProgramTraineeTaskDto>> GetTasksForTraineeInProgram(int programId, int traineeId)
    {
        return await _db.TraineeTaskAssignments
            .Where(x => x.TaskItem.ProgramId == programId && x.TraineeId == traineeId)
            .Select(x => new ProgramTraineeTaskDto
            {
                TaskId = x.TaskItemId,
                Title = x.TaskItem.Title,
                Description = x.TaskItem.Description,
                Status = x.Status.ToString()
            })
            .ToListAsync();
    }



public async Task<List<User>> GetAvailableMentorsAsync(int programId)
{
    var assignedMentorIds = await _db.TrainingProgramMentors
        .Where(x => x.ProgramId == programId)
        .Select(x => x.MentorId)
        .ToListAsync();

    return await _db.Users
        .Where(u => u.Role == UserRole.Mentor && !assignedMentorIds.Contains(u.Id))
        .ToListAsync();
}


    public async Task<bool> AssignMentorAsync(int programId, int mentorId)
    {
        var exists = await _db.TrainingProgramMentors
            .AnyAsync(x => x.ProgramId == programId && x.MentorId == mentorId);

        if (exists)
            return false;

        var entity = new TrainingProgramMentor
        {
            ProgramId = programId,
            MentorId = mentorId
        };

        _db.TrainingProgramMentors.Add(entity);
        await _db.SaveChangesAsync();
        return true;
    }



    public async Task<bool> AssignTraineeWithMentorAsync(AssignTraineeDto dto)
    {
        // 1. Validate program
        var program = await _db.TrainingPrograms
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == dto.ProgramId);

        if (program == null) return false;

        // 2. Validate trainee exists
        var trainee = await _db.Users
            .FirstOrDefaultAsync(u => u.Id == dto.TraineeId && u.Role == UserRole.Trainee);

        if (trainee == null) return false;

        // 3. Add trainee to program if not already mapped
        var existingTP = await _db.TraineePrograms
            .AnyAsync(x => x.ProgramId == dto.ProgramId && x.TraineeId == dto.TraineeId);

        if (!existingTP)
        {
            _db.TraineePrograms.Add(new TraineeProgram
            {
                ProgramId = dto.ProgramId,
                TraineeId = dto.TraineeId
            });

            // Auto-create task assignments
            var tasks = await _db.TaskItems
                .Where(t => t.ProgramId == dto.ProgramId)
                .ToListAsync();

            foreach (var task in tasks)
            {
                _db.TraineeTaskAssignments.Add(new TraineeTaskAssignment
                {
                    TraineeId = dto.TraineeId,
                    TaskItemId = task.Id,
                    Status = TrainingMentorship.Domain.Enums.TaskStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        // 4. Always link mentor ↔ trainee
        foreach (var mentorId in dto.MentorIds)
        {
            // Mentor must belong to this program
            var mentorOk = await _db.TrainingProgramMentors
                .AnyAsync(x => x.ProgramId == dto.ProgramId && x.MentorId == mentorId);

            if (!mentorOk) continue;

            var exists = await _db.ProgramMentorTrainees
                .AnyAsync(x => x.ProgramId == dto.ProgramId &&
                               x.MentorId == mentorId &&
                               x.TraineeId == dto.TraineeId);

            if (!exists)
            {
                _db.ProgramMentorTrainees.Add(new ProgramMentorTrainee
                {
                    ProgramId = dto.ProgramId,
                    MentorId = mentorId,
                    TraineeId = dto.TraineeId
                });
            }
        }

        return await _db.SaveChangesAsync() > 0;
    }


    public async Task<List<TrainingProgramMentor>> GetMentorsForProgramAsync(int programId)
    {
        return await _db.TrainingProgramMentors
            .Include(x => x.Mentor)
            .Where(x => x.ProgramId == programId)
            .ToListAsync();
    }


    public async Task<List<User>> GetAvailableTraineesAsync(int programId)
    {
        var assignedIds = await _db.TraineePrograms
            .Where(x => x.ProgramId == programId)
            .Select(x => x.TraineeId)
            .ToListAsync();

        return await _db.Users
            .Where(x => x.Role == UserRole.Trainee && !assignedIds.Contains(x.Id))
            .ToListAsync();
    }



    public async Task<List<TrainingProgram>> GetProgramsByMentorIdAsync(int mentorId)
    {
        return await _db.TrainingProgramMentors
            .Where(x => x.MentorId == mentorId)
            .Select(x => x.Program)   // navigation property
            .ToListAsync();
    }




    public async Task<List<TrainingProgram>> GetProgramsForTraineeAsync(int traineeId)
    {
        return await _db.ProgramMentorTrainees
            .Where(x => x.TraineeId == traineeId)
            .Include(x => x.Program)
                .ThenInclude(p => p.Tasks)
            .Include(x => x.Program)
                .ThenInclude(p => p.Schedules)
            .Select(x => x.Program)
            .Distinct()
            .ToListAsync();
    }





    public async Task<bool> CreateTaskAsync(CreateTaskDto dto)
    {
        // 1️⃣ Create and save the task
        var task = new TaskItem
        {
            ProgramId = dto.ProgramId,
            Title = dto.Title,
            Description = dto.Description
        };

        _db.TaskItems.Add(task);
        await _db.SaveChangesAsync();

        // 2️⃣ Get all trainees already in the program
        var trainees = await _db.TraineePrograms
            .Where(tp => tp.ProgramId == dto.ProgramId)
            .Select(tp => tp.TraineeId)
            .ToListAsync();

        // 3️⃣ Assign the newly created task to all trainees
        foreach (var traineeId in trainees)
        {
            _db.TraineeTaskAssignments.Add(new TraineeTaskAssignment
            {
                TraineeId = traineeId,
                TaskItemId = task.Id,
                Status = TrainingMentorship.Domain.Enums.TaskStatus.Pending,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _db.SaveChangesAsync();
        return true;
    }


    public async Task<bool> UpdateTaskStatusAsync(UpdateTaskStatusDto dto)
    {
        var assignment = await _db.TraineeTaskAssignments
            .FirstOrDefaultAsync(x =>
                x.TraineeId == dto.TraineeId &&
                x.TaskItemId == dto.TaskItemId);

        if (assignment == null)
            return false;

        

        assignment.Status = Enum.Parse<TrainingMentorship.Domain.Enums.TaskStatus>(dto.Status); ;
        assignment.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }



}


