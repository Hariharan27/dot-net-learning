using System;
namespace TrainingMentorship.Application.DTOs.TrainingProgram;

public class ProgramMentorDto
{
    public int MentorId { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int TraineeCount { get; set; }

}

