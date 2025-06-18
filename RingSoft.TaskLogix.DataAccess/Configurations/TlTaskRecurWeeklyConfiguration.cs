using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskRecurWeeklyConfiguration : IEntityTypeConfiguration<TlTaskRecurWeekly>
    {
        public void Configure(EntityTypeBuilder<TlTaskRecurWeekly> builder)
        {
            builder.Property(p => p.TaskId).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.RecurType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.RecurWeeks).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.Sunday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.Monday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.Tuesday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.Wednesday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.Thursday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.Friday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.Saturday).HasColumnType(DbConstants.BoolColumnType);
            builder.Property(p => p.RegenWeeksAfterCompleted).HasColumnType(DbConstants.IntegerColumnType);

            builder.HasOne(p => p.Task)
                .WithMany(p => p.RecurWeekly)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
