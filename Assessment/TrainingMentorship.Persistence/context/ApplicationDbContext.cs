using System;
using Microsoft.EntityFrameworkCore;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<TrainingProgram> TrainingPrograms { get; set; }
    public DbSet<TrainingProgramMentor> TrainingProgramMentors { get; set; }
    public DbSet<TraineeProgram> TraineePrograms { get; set; }
    public DbSet<ProgramMentorTrainee> ProgramMentorTrainees { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<TraineeTaskAssignment> TraineeTaskAssignments { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<ScheduleItem> ScheduleItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply Fluent Configurations from folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}


