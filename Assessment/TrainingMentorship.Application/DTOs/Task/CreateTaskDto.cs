using System;
namespace TrainingMentorship.Application.DTOs.Task;

public class CreateTaskDto
{
    public int ProgramId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

}

