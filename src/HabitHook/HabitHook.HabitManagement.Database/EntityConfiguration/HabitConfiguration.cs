using HabitHook.HabitManagement.Domain.Core.Habits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitHook.HabitManagement.Database.EntityConfiguration;

public sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("varchar");

        builder.Property(h => h.TargetValue)
            .IsRequired()
            .HasColumnType("int");

        // builder.HasQueryFilter(h => !h.IsDeleted);
    }
}