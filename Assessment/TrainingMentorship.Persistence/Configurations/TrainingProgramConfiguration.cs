using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
{
    public void Configure(EntityTypeBuilder<TrainingProgram> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasMany(x => x.Mentors)
            .WithOne(x => x.Program)
            .HasForeignKey(x => x.ProgramId);

        builder.HasMany(x => x.Trainees)
            .WithOne(x => x.Program)
            .HasForeignKey(x => x.ProgramId);

        builder.HasMany(x => x.MentorTraineeLinks)
            .WithOne(x => x.Program)
            .HasForeignKey(x => x.ProgramId);

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Program)
            .HasForeignKey(x => x.ProgramId);

        builder.HasMany(x => x.Schedules)
            .WithOne(x => x.Program)
            .HasForeignKey(x => x.ProgramId);
    }
}


