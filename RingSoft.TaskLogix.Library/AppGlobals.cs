using RingSoft.App.Library;
using RingSoft.CustomTemplate.Library;
using RingSoft.TaskLogix.DataAccess;
using RingSoft.TaskLogix.Library.ViewModels;
using RingSoft.TaskLogix.Sqlite;
using RingSoft.TaskLogix.SqlServer;

namespace RingSoft.TaskLogix.Library
{
    public class AppGlobals : CustomTemplateAppGlobals
    {
        public override string AppTitle => "TaskLogix";
        public override string AppCopyright => "©2025 by Peter Ringering";
        public override string AppGuid => "c0cad4a3-57e1-474c-ac1f-7e9e04cdc9c3";
        public override string AppItem => "Task Data";
        public override string AppDemoName => "Demonstration Data";
        public override int AppVersionId => 372;
        public override string DebugFolder => GetDebugFolder();

        public string GetDebugFolder()
        {
            var result = string.Empty;
#if DEBUG
            result = RingSoftAppGlobals.AssemblyDirectory;
#endif
            return result;
        }

        public static TaskLogixLookupContext LookupContext { get; set; }

        public static new MainViewModel MainViewModel { get; set; }

        public override CustomTemplateLookupContext GetNewLookupContext()
        {
            LookupContext = new TaskLogixLookupContext();
            return LookupContext;
        }

        public override CustomTemplateSqliteDbContext GetNewSqliteDbContext()
        {
            return new TaskLogixSqliteDbContext();
        }

        public override CustomTemplateSqlServerDbContext GetNewSqlServerDbContext()
        {
            return new TaskLogixSqlServerDbContext();
        }
    }
}
