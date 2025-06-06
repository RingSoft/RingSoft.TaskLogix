using Microsoft.EntityFrameworkCore.Design;

namespace RingSoft.TaskLogix.Sqlite
{
    public class ContextDesignTimeFactory : IDesignTimeDbContextFactory<TaskLogixSqliteDbContext>
    {
        TaskLogixSqliteDbContext IDesignTimeDbContextFactory<TaskLogixSqliteDbContext>
            .CreateDbContext(string[] args)
        {
            return new TaskLogixSqliteDbContext() { IsDesignTime = true };
        }
    }
}
