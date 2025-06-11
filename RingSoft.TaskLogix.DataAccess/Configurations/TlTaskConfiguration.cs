using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskConfiguration : IEntityTypeConfiguration<TlTask>
    {
        public void Configure(EntityTypeBuilder<TlTask> builder)
        {
            builder.Property(p => p.Id).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.Subject).HasColumnType(DbConstants.StringColumnType);
            builder.Property(p => p.StartDate).HasColumnType(DbConstants.DateColumnType);
            builder.Property(p => p.DueDate).HasColumnType(DbConstants.DateColumnType);
            builder.Property(p => p.ReminderDateTime).HasColumnType(DbConstants.DateColumnType);
            builder.Property(p => p.StatusType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.PriorityType).HasColumnType(DbConstants.ByteColumnType);
            builder.Property(p => p.PercentComplete).HasColumnType(DbConstants.DecimalColumnType);
        }
    }
}
