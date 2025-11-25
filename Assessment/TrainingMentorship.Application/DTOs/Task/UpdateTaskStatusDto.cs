using System;
using TrainingMentorship.Domain.Enums;
namespace TrainingMentorship.Application.DTOs.Task;

public class UpdateTaskStatusDto
{
    public int TraineeId { get; set; }
    public int TaskItemId { get; set; }
    public string Status { get; set; } = default!;

}

