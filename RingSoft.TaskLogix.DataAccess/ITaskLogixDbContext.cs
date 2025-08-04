using Microsoft.EntityFrameworkCore;
using RingSoft.CustomTemplate.Library;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess
{
    public interface ITaskLogixDbContext : ITemplateDbContext
    {
        public DbSet<TlTask> Tasks { get; set; }

        public DbSet<TlTaskRecurDaily> TaskRecurDailys { get; set; }

        public DbSet<TlTaskRecurWeekly> TaskRecurWeeklys { get; set; }

        public DbSet<TlTaskRecurMonthly> TaskRecurMonthlys { get; set; }

        public DbSet<TlTaskRecurYearly> TaskRecurYearlys { get; set; }

        public DbSet<TlTaskHistory> TaskHistory { get; set; }
    }
}
