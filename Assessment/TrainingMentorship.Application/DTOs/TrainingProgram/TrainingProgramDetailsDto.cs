using System;
namespace TrainingMentorship.Application.DTOs.TrainingProgram;

public class TrainingProgramDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public int MentorCount { get; set; }
    public int TraineeCount { get; set; }
    public int TaskCount { get; set; }

    public List<ProgramMentorDto> Mentors { get; set; } = new();

    public List<ProgramTraineeDto> Trainees { get; set; } = new();

    public List<ProgramTraineeTaskDto> Tasks { get; set; } = new();



}

