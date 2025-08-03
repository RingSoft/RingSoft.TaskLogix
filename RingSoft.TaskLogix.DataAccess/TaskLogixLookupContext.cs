using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.Lookup;
using RingSoft.DbLookup.ModelDefinition;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess
{
    public class TaskLogixLookupContext : CustomTemplateLookupContext
    {
        public TableDefinition<TlTask> Tasks { get; set; }

        public TableDefinition<TlTaskRecurDaily> TaskRecurDailys { get; set; }

        public TableDefinition<TlTaskRecurWeekly> TaskRecurWeeklys { get; set; }

        public LookupDefinition<TaskLookup, TlTask> TaskLookupDefinition { get; set; }

        public LookupDefinition<TaskRecurDailyLookup, TlTaskRecurDaily> TaskRecurDailyLookupDefinition { get; set; }

        public LookupDefinition<TaskRecurWeeklyLookup, TlTaskRecurWeekly> TaskRecurWeeklyLookupDefinition { get; set; }

        protected override void SetupTemplateLookupDefinitions()
        {
            TaskLookupDefinition = new LookupDefinition<TaskLookup, TlTask>(Tasks);

            TaskLookupDefinition.AddVisibleColumnDefinition(
                p => p.Subject
                , "Subject"
                , p => p.Subject, 70);

            TaskLookupDefinition.AddVisibleColumnDefinition(
                p => p.DueDate
                , "Start Date"
                , p => p.StartDate, 30);

            Tasks.HasLookupDefinition(TaskLookupDefinition);

            TaskRecurDailyLookupDefinition =
                new LookupDefinition<TaskRecurDailyLookup, TlTaskRecurDaily>(TaskRecurDailys);

            TaskRecurDailyLookupDefinition.Include(p => p.Task)
                .AddVisibleColumnDefinition(
                    p => p.Task
                    , "Task"
                    , p => p.Subject, 50);

            TaskRecurDailyLookupDefinition.AddVisibleColumnDefinition(
                p => p.RecurType
                , "Recur Type"
                , p => p.RecurType, 20);

            TaskRecurDailys.HasLookupDefinition(TaskRecurDailyLookupDefinition);

            TaskRecurWeeklyLookupDefinition =
                new LookupDefinition<TaskRecurWeeklyLookup, TlTaskRecurWeekly>(TaskRecurWeeklys);

            TaskRecurWeeklyLookupDefinition.Include(p => p.Task)
                .AddVisibleColumnDefinition(
                    p => p.Task
                    , "Task"
                    , p => p.Subject, 50);

            TaskRecurWeeklyLookupDefinition.AddVisibleColumnDefinition(
                p => p.RecurType
                , "Recur Type"
                , p => p.RecurType, 20);

            TaskRecurWeeklys.HasLookupDefinition(TaskRecurWeeklyLookupDefinition);

        }

        protected override void SetupTemplateModel()
        {
            Tasks.HasDescription("Tasks").HasRecordDescription("Task");

            TaskRecurDailys.HasDescription("Task Recur Dailys").HasRecordDescription("Recur Daily");

            TaskRecurDailys.GetFieldDefinition(p => p.RecurType)
                .IsEnum<DailyRecurTypes>();
        }
    }
}
