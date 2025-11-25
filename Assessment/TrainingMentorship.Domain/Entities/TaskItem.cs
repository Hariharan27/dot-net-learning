using System;
namespace TrainingMentorship.Domain.Entities;

public class TaskItem: BaseEntity
{

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    // Every task belongs to a program
    public int ProgramId { get; set; }
    public TrainingProgram Program { get; set; } = default!;

    // When trainees join the program → they get assignment records
    public ICollection<TraineeTaskAssignment> TraineeAssignments { get; set; } = new List<TraineeTaskAssignment>();

}

