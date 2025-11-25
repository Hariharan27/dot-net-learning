using System;
namespace TrainingMentorship.Domain.Entities;

public class TrainingProgramMentor : BaseEntity
{
    public int ProgramId { get; set; }
    public TrainingProgram Program { get; set; } = default!;

    public int MentorId { get; set; }
    public User Mentor { get; set; } = default!;

}

