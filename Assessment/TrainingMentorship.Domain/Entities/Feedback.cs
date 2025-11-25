using System;
namespace TrainingMentorship.Domain.Entities;

public class Feedback : BaseEntity
{
    public int AssignmentId { get; set; }
    public TraineeTaskAssignment Assignment { get; set; } = default!;

    public int MentorId { get; set; }
    public User Mentor { get; set; } = default!;

    public string Comments { get; set; } = default!;
    public int Rating { get; set; }   // 1–5

}

