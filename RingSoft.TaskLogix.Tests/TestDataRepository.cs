using RingSoft.App.Library;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Testing;
using RingSoft.TaskLogix.DataAccess;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Sqlite;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace RingSoft.TaskLogix.Tests
{
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