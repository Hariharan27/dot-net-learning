using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class TraineeTaskAssignmentConfiguration : IEntityTypeConfiguration<TraineeTaskAssignment>
{
    public void Configure(EntityTypeBuilder<TraineeTaskAssignment> builder)
    {
        // Trainee FK → User
        builder.HasOne(x => x.Trainee)
            .WithMany()
            .HasForeignKey(x => x.TraineeId)
            .OnDelete(DeleteBehavior.Restrict);    // IMPORTANT

        // Task FK → TaskItem
        builder.HasOne(x => x.TaskItem)
            .WithMany(x => x.TraineeAssignments)
            .HasForeignKey(x => x.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);     // SAFE (Task owns assignments)

        // Assignment → Feedback (child table)
        builder.HasMany(x => x.Feedbacks)
            .WithOne(x => x.Assignment)
            .HasForeignKey(x => x.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);     // SAFE
    }
}



