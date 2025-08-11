using Microsoft.EntityFrameworkCore;
using RingSoft.App.Library;
using RingSoft.CustomTemplate.Library;
using RingSoft.CustomTemplate.MasterData;
using RingSoft.DbLookup;
using RingSoft.DbLookup.DataProcessor;
using RingSoft.DbLookup.EfCore;
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

        public static bool FirstTime { get; private set; } = true;

        public TaskLogixTestDataRepository(TestDataRegistry context) : base(context)
        {
            DataContext = context;
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTask>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurDaily>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurWeekly>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurMonthly>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskRecurYearly>());
            DataContext.AddEntity(new DataRepositoryRegistryItem<TlTaskHistory>());
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
    }
}