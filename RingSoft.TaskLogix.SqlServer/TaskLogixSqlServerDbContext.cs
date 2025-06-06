using Microsoft.EntityFrameworkCore;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess;

namespace RingSoft.TaskLogix.SqlServer
{
    public class TaskLogixSqlServerDbContext : CustomTemplateSqlServerDbContext, ITaskLogixDbContext
    {
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
