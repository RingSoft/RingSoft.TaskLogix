using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Sqlite
{
    public class TaskLogixSqliteDbContext : CustomTemplateSqliteDbContext, ITaskLogixDbContext
    {
        public DbSet<TlTask> Tasks { get; set; }
        public DbSet<TlTaskRecurDaily> TaskRecurDailys { get; set; }
        public DbSet<TlTaskRecurWeekly> TaskRecurWeeklys { get; set; }
        public DbSet<TlTaskRecurMonthly> TaskRecurMonthlys { get; set; }
        public DbSet<TlTaskRecurYearly> TaskRecurYearlys { get; set; }
        public DbSet<TlTaskHistory> TaskHistory { get; set; }

        public override DbContextEfCore GetNewDbContextEfCore()
        {
            return new TaskLogixSqliteDbContext();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbConstants.ConstantGenerator = new SqliteDbConstants();
            DataAccessGlobals.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
