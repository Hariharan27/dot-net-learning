using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingMentorship.Domain.Entities;

namespace TrainingMentorship.Persistence.Configurations;

public class ScheduleItemConfiguration
{
    public void Configure(EntityTypeBuilder<ScheduleItem> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
    }

}

