using HabitHook.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitHook.HabitManagement.Database.EntityConfiguration;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.Property(b => b.CreatedOn)
            .IsRequired();

        builder.Property(b => b.CreatedBy)
            .IsRequired(false)
            .HasMaxLength(120)
            .HasColumnType("varchar");

        builder.Property(b => b.LastModifiedOn)
            .IsRequired(false);

        builder.Property(b => b.LastModifiedBy)
            .IsRequired(false)
            .HasMaxLength(120)
            .HasColumnType("varchar");

        builder.Property(b => b.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}