using Microsoft.EntityFrameworkCore.Design;

namespace RingSoft.TaskLogix.SqlServer
{
    public class ContextDesignTimeFactory : IDesignTimeDbContextFactory<TaskLogixSqlServerDbContext>
    {
        public TaskLogixSqlServerDbContext CreateDbContext(string[] args)
        {
            return new TaskLogixSqlServerDbContext() { IsDesignTime = true };
        }
    }
}
