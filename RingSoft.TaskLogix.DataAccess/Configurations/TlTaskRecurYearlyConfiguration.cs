using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskRecurYearlyConfiguration : IEntityTypeConfiguration<TlTaskRecurYearly>
    {
        public void Configure(EntityTypeBuilder<TlTaskRecurYearly> builder)
        {
            builder.Property(p => p.TaskId).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.RecurType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.EveryMonthType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.MonthDay).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.WeekType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.DayType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.WeekMonthType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.RegenYearsAfterCompleted).HasColumnType(DbConstants.IntegerColumnType);

            builder.HasOne(p => p.Task)
                .WithMany(p => p.RecurYearly)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
