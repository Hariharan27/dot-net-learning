using System;
namespace TrainingMentorship.Domain.Entities;

public class TrainingProgram : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<TrainingProgramMentor> Mentors { get; set; }
    = new List<TrainingProgramMentor>();

    public ICollection<TraineeProgram> Trainees { get; set; }
        = new List<TraineeProgram>();

    public ICollection<ProgramMentorTrainee> MentorTraineeLinks { get; set; }
        = new List<ProgramMentorTrainee>();

    public ICollection<TaskItem> Tasks { get; set; }
        = new List<TaskItem>();

    public ICollection<ScheduleItem> Schedules { get; set; }
        = new List<ScheduleItem>();

}

