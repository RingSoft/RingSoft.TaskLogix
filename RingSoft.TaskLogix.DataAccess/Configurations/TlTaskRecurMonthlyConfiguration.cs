using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskRecurMonthlyConfiguration : IEntityTypeConfiguration<TlTaskRecurMonthly>
    {
        public void Configure(EntityTypeBuilder<TlTaskRecurMonthly> builder)
        {
            builder.Property(p => p.TaskId).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.RecurType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.DayXOfEvery).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.OfEveryYMonths).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.WeekType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.DayType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.OfEveryWeekTypeMonths).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.RegenMonthsAfterCompleted).HasColumnType(DbConstants.IntegerColumnType);

            builder.HasOne(p => p.Task)
                .WithMany(p => p.RecurMonthly)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
