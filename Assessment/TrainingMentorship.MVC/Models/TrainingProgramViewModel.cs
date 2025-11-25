using System;
namespace TrainingMentorship.MVC.Models;

public class TrainingProgramViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int MentorCount { get; set; }
    public int TraineeCount { get; set; }
}

