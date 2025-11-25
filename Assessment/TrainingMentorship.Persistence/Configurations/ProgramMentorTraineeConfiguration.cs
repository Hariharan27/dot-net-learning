using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class ProgramMentorTraineeConfiguration : IEntityTypeConfiguration<ProgramMentorTrainee>
{
    public void Configure(EntityTypeBuilder<ProgramMentorTrainee> builder)
    {
        builder.HasKey(x => x.Id);

        // Program FK
        builder.HasOne(x => x.Program)
            .WithMany(x => x.MentorTraineeLinks)
            .HasForeignKey(x => x.ProgramId)
            .OnDelete(DeleteBehavior.Cascade);   // Safe — Program owns this link

        // Mentor FK → User
        builder.HasOne(x => x.Mentor)
            .WithMany()
            .HasForeignKey(x => x.MentorId)
            .OnDelete(DeleteBehavior.Restrict); // IMPORTANT

        // Trainee FK → User
        builder.HasOne(x => x.Trainee)
            .WithMany()
            .HasForeignKey(x => x.TraineeId)
            .OnDelete(DeleteBehavior.Restrict); // IMPORTANT
    }
}


