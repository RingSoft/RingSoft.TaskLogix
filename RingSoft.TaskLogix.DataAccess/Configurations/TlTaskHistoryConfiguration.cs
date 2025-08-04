using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskHistoryConfiguration : IEntityTypeConfiguration<TlTaskHistory>
    {
        public void Configure(EntityTypeBuilder<TlTaskHistory> builder)
        {
            builder.Property(p => p.Id).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.TaskId).HasColumnType(DbConstants.IntegerColumnType);
            builder.Property(p => p.StartDate).HasColumnType(DbConstants.DateColumnType);
            builder.Property(p => p.DueDate).HasColumnType(DbConstants.DateColumnType);
            builder.Property(p => p.CompletionDate).HasColumnType(DbConstants.DateColumnType);

            builder.HasOne(p => p.Task)
                .WithMany(p => p.History)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
