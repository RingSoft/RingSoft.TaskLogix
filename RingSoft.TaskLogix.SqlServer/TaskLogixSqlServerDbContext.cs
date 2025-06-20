using Microsoft.EntityFrameworkCore;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.SqlServer
{
    public class TaskLogixSqlServerDbContext : CustomTemplateSqlServerDbContext, ITaskLogixDbContext
    {
        public DbSet<TlTask> Tasks { get; set; }
        public DbSet<TlTaskRecurDaily> TaskRecurDailys { get; set; }
        public DbSet<TlTaskRecurWeekly> TaskRecurWeeklys { get; set; }

        public override DbContextEfCore GetNewDbContextEfCore()
        {
            return new TaskLogixSqlServerDbContext();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbConstants.ConstantGenerator = new SqlServerDbConstants();
            DataAccessGlobals.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
