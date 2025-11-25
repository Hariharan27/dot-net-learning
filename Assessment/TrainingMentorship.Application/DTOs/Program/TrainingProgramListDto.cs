using System;
namespace TrainingMentorship.Application.DTOs.Program;

public class TrainingProgramListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
}

