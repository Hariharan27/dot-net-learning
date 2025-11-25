using System;
namespace TrainingMentorship.MVC.Models;

public class CreateTaskViewModel
{
    public int ProgramId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

}

