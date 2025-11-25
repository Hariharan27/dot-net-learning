using System;
namespace TrainingMentorship.Application.DTOs.TrainingProgram;

public class ProgramTraineeDto
{
    public int TraineeId { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;

}

