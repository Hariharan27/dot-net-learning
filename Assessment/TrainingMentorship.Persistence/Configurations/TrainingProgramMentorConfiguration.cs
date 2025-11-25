using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class TrainingProgramMentorConfiguration : IEntityTypeConfiguration<TrainingProgramMentor>
{
    public void Configure(EntityTypeBuilder<TrainingProgramMentor> builder)
    {
        builder.HasKey(x => x.Id);

        // Program FK
        builder.HasOne(x => x.Program)
            .WithMany(x => x.Mentors)
            .HasForeignKey(x => x.ProgramId)
            .OnDelete(DeleteBehavior.Cascade);   // A program owns its mentors

        // Mentor FK → User
        builder.HasOne(x => x.Mentor)
            .WithMany()
            .HasForeignKey(x => x.MentorId)
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascade loops
    }
}

