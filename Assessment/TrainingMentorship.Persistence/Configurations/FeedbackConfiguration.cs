using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;


public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.Property(x => x.Comments)
            .HasMaxLength(500);

        // FK: Feedback → Mentor (User)
        builder.HasOne(x => x.Mentor)
            .WithMany()
            .HasForeignKey(x => x.MentorId)
            .OnDelete(DeleteBehavior.Restrict);   // <-- IMPORTANT

        // FK: Feedback → Assignment
        builder.HasOne(x => x.Assignment)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);    // <-- SAFE
    }
}


