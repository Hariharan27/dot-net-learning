using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class TraineeProgramConfiguration : IEntityTypeConfiguration<TraineeProgram>
{
    public void Configure(EntityTypeBuilder<TraineeProgram> builder)
    {
        builder.HasKey(x => x.Id);

        // Program FK
        builder.HasOne(x => x.Program)
            .WithMany(x => x.Trainees)
            .HasForeignKey(x => x.ProgramId)
            .OnDelete(DeleteBehavior.Cascade); // Program owns trainee-assignments

        // Trainee FK → User
        builder.HasOne(x => x.Trainee)
            .WithMany()
            .HasForeignKey(x => x.TraineeId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade loops (User is involved in many tables)
    }
}
