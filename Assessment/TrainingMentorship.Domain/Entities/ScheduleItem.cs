using System;
namespace TrainingMentorship.Domain.Entities;

public class ScheduleItem : BaseEntity
{
    public int ProgramId { get; set; }
    public TrainingProgram Program { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ScheduledAt { get; set; }

}

