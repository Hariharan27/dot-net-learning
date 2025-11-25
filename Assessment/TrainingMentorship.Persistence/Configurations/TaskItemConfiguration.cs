using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(x => x.TraineeAssignments)
            .WithOne(x => x.TaskItem)
            .HasForeignKey(x => x.TaskItemId);
    }
}


