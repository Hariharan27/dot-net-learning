using System;
namespace TrainingMentorship.Application.DTOs.TrainingProgram;

public class AssignTraineeDto
{
    public int ProgramId { get; set; }
    public int TraineeId { get; set; }
    public List<int> MentorIds { get; set; } = new();

}

