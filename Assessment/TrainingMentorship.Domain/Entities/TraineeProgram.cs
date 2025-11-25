using System;
namespace TrainingMentorship.Domain.Entities;

public class TraineeProgram : BaseEntity
{
    public int ProgramId { get; set; }
    public TrainingProgram Program { get; set; } = default!;

    public int TraineeId { get; set; }
    public User Trainee { get; set; } = default!;

}

