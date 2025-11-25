using System;
using TrainingMentorship.Domain.Enums;
namespace TrainingMentorship.Domain.Entities;

public class TraineeTaskAssignment: BaseEntity
{
    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; } = default!;

    public int TraineeId { get; set; }
    public User Trainee { get; set; } = default!;

    public TrainingMentorship.Domain.Enums.TaskStatus Status { get; set; } = TrainingMentorship.Domain.Enums.TaskStatus.Pending;
    public DateTime? CompletedAt { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

}

