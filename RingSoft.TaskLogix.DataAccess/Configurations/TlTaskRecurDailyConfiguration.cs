using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess.Configurations
{
    public class TlTaskRecurDailyConfiguration : IEntityTypeConfiguration<TlTaskRecurDaily>
    {
        public void Configure(EntityTypeBuilder<TlTaskRecurDaily> builder)
        {
            builder.HasOne(p => p.Task)
                .WithMany(p => p.RecurDaily)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
