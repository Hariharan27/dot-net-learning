using System;
using TrainingMentorship.Application.DTOs.Auth;
using TrainingMentorship.Application.DTOs.TrainingProgram;

namespace TrainingMentorship.MVC.Models;

public class AssignTraineeViewModel
{
    public int ProgramId { get; set; }

    // Selected trainee
    public int? TraineeId { get; set; }

    // MULTI-SELECT mentors → list of selected mentor IDs
    public List<int> MentorIds { get; set; } = new();

    // Dropdown data sources
    public List<UserDto> AvailableTrainees { get; set; } = new();

    public List<ProgramMentorDto> Mentors { get; set; } = new();


}

