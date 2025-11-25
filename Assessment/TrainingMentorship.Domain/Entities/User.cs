using System;
using TrainingMentorship.Domain.Enums;

namespace TrainingMentorship.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserRole Role { get; set; }

    // Navigation
    public ICollection<TrainingProgramMentor> MentorPrograms { get; set; }
        = new List<TrainingProgramMentor>();

    public ICollection<TraineeProgram> TraineePrograms { get; set; }
        = new List<TraineeProgram>();

    public ICollection<ProgramMentorTrainee> ProgramMentorTrainees { get; set; }
        = new List<ProgramMentorTrainee>();


}

