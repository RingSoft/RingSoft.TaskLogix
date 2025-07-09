using Microsoft.EntityFrameworkCore;
using RingSoft.App.Library;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Testing;
using RingSoft.TaskLogix.DataAccess;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.Processors;
using RingSoft.TaskLogix.Sqlite;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace RingSoft.TaskLogix.Tests
{
    public enum TaskIds
    {
        DefaultWeeklyRecur = 1,
    }
    public class TestDataRegistry : DataRepositoryRegistry
    {
    }

    public class TaskLogixTestDataRepository : TestDataRepository
    {
        public new TestDataRegistry DataContext { get; }

        public TaskLogixTestDataRepository(TestDataRegistry context) : base(context)
        {
            DataContext = context;
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTask>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurDaily>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurWeekly>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurMonthly>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurYearly>());
        }

        public void Initialize()
        {
            AppGlobals.UnitTesting = true;
            AppGlobals.LookupContext = new TaskLogixLookupContext();
            var sqliteContext = new TaskLogixSqliteDbContext()
            {
                IsDesignTime = true,
            };
            AppGlobals.LookupContext.SetDbContext(sqliteContext);
            AppGlobals.LookupContext.Initialize();
        }

        public void SaveTlTask(TlTask task, TaskProcessor taskProcessor)
        {
            task.Subject = "Test";
            task.DueDate = taskProcessor.StartDate;
            if (taskProcessor.ReminderDateTime.HasValue)
            {
                task.SnoozeDateTime = taskProcessor.ReminderDateTime.Value;
            }

            taskProcessor.SaveProcessor(task);
        }

        public void LoadFromTlTask(int taskId, TaskProcessor taskProcessor)
        {
            var context = SystemGlobals.DataRepository.GetDataContext();

            var tlTask = context.GetTable<TlTask>()
                .Include(p => p.RecurDaily)
                .Include(p => p.RecurWeekly)
                .Include(p => p.RecurMonthly)
                .Include(p => p.RecurYearly)
                .FirstOrDefault(p => p.Id == taskId);

            taskProcessor.LoadProcessor(tlTask);
        }
    }
}