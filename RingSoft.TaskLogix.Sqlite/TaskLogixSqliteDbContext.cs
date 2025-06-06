using Microsoft.EntityFrameworkCore;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.EfCore;
using RingSoft.TaskLogix.DataAccess;

namespace RingSoft.TaskLogix.Sqlite
{
    public class TaskLogixSqliteDbContext : CustomTemplateSqliteDbContext, ITaskLogixDbContext
    {
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

    }
}
