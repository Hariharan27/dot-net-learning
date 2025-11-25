using System;
using TrainingMentorship.Application.DTOs.Task;
using TrainingMentorship.Application.DTOs.TrainingProgram;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Application.interfaces;

public interface ITrainingProgramRepository
{

    Task<int> CreateAsync(TrainingProgram program);
    Task<List<TrainingProgram>> GetAllAsync();
    Task<TrainingProgram?> GetByIdAsync(int id);
    Task<TrainingProgram?> GetProgramDetailsForMentorAsync(int programId, int mentorId);
    Task<bool> UpdateAsync(TrainingProgram program);
    Task<List<ProgramTraineeDto>> GetTraineesForMentorAsync(int programId, int mentorId);
    Task<List<ProgramTraineeTaskDto>> GetTasksForTraineeInProgram(int programId, int traineeId);
    Task<List<User>> GetAvailableMentorsAsync(int programId);
    Task<bool> AssignMentorAsync(int programId, int mentorId);
    Task<bool> AssignTraineeWithMentorAsync(AssignTraineeDto dto);
    Task<List<TrainingProgramMentor>> GetMentorsForProgramAsync(int programId);
    Task<List<User>> GetAvailableTraineesAsync(int programId);
    Task<List<TrainingProgram>> GetProgramsByMentorIdAsync(int mentorId);
    Task<List<TrainingProgram>> GetProgramsForTraineeAsync(int traineeId);
    Task<bool> CreateTaskAsync(CreateTaskDto dto);
    Task<bool> UpdateTaskStatusAsync(UpdateTaskStatusDto dto);


}

