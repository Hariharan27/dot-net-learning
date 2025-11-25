using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FullName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        // Relations
        builder.HasMany(x => x.MentorPrograms)
            .WithOne(x => x.Mentor)
            .HasForeignKey(x => x.MentorId);

        builder.HasMany(x => x.TraineePrograms)
            .WithOne(x => x.Trainee)
            .HasForeignKey(x => x.TraineeId);

        builder.HasMany(x => x.ProgramMentorTrainees)
            .WithOne(x => x.Trainee)
            .HasForeignKey(x => x.TraineeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

