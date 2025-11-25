using System;
namespace TrainingMentorship.Application.DTOs.TrainingProgram;

public class ProgramTraineeTaskDto
{
    public int TaskId { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string Status { get; set; } = default!;

}

