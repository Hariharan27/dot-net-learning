using System;
namespace TrainingMentorship.MVC.Models;

public class UpdateTaskStatusViewModel
{
    public int ProgramId { get; set; }
    public int TaskItemId { get; set; }
    public string Status { get; set; } = default!;

}

