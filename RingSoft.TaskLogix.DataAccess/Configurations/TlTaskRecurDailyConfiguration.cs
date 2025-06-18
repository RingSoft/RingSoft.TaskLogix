using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.DbLookup.EfCore;
using RingSoft.DbLookup.QueryBuilder;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskRecurDailyConfiguration : IEntityTypeConfiguration<TlTaskRecurDaily>
    {
        public void Configure(EntityTypeBuilder<TlTaskRecurDaily> builder)
        {
            builder.Property(p => p.TaskId).HasColumnType(DbConstants.IntegerColumnType);
            //builder.Property(p => p.RowId).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.RecurType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.RecurDays).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.RegenDaysAfterCompleted).HasColumnType(DbConstants.IntegerColumnType);

            //builder.HasKey(p => new { p.TaskId, p.RowId });

            builder.HasOne(p => p.Task)
                .WithMany(p => p.RecurDaily)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
