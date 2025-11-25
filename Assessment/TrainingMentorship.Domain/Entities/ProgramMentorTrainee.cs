using System;
namespace TrainingMentorship.Domain.Entities;

public class ProgramMentorTrainee : BaseEntity
{
    public int ProgramId { get; set; }
    public TrainingProgram Program { get; set; } = default!;

    public int MentorId { get; set; }
    public User Mentor { get; set; } = default!;

    public int TraineeId { get; set; }
    public User Trainee { get; set; } = default!;

}

