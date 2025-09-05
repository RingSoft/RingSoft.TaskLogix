using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.Lookup;
using RingSoft.DbLookup.ModelDefinition;
using RingSoft.DbLookup.QueryBuilder;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess
{
    public class TaskLogixLookupContext : CustomTemplateLookupContext
    {
        public TableDefinition<TlTask> Tasks { get; set; }

        public TableDefinition<TlTaskRecurDaily> TaskRecurDailys { get; set; }

        public TableDefinition<TlTaskRecurWeekly> TaskRecurWeeklys { get; set; }

        public TableDefinition<TlTaskRecurMonthly> TaskRecurMonthlys { get; set; }

        public TableDefinition<TlTaskRecurYearly> TaskRecurYearlys { get; set; }

        public TableDefinition<TlTaskHistory> TaskHistory { get; set; }

        public LookupDefinition<TaskLookup, TlTask> TaskLookupDefinition { get; set; }

        public LookupDefinition<TaskRecurDailyLookup, TlTaskRecurDaily> TaskRecurDailyLookupDefinition { get; set; }

        public LookupDefinition<TaskRecurWeeklyLookup, TlTaskRecurWeekly> TaskRecurWeeklyLookupDefinition { get; set; }

        public LookupDefinition<TaskRecurMonthlyLookup, TlTaskRecurMonthly> TaskRecurMonthlyLookupDefinition { get; set; }

        public LookupDefinition<TaskRecurYearlyLookup, TlTaskRecurYearly> TaskRecurYearlyLookupDefinition { get; set; }

        public LookupDefinition<TaskHistoryLookup, TlTaskHistory> TaskHistoryLookupDefinition { get; set; }

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

            TaskRecurMonthlyLookupDefinition =
                new LookupDefinition<TaskRecurMonthlyLookup, TlTaskRecurMonthly>(TaskRecurMonthlys);

            TaskRecurMonthlyLookupDefinition.Include(p => p.Task)
                .AddVisibleColumnDefinition(
                    p => p.Task
                    , "Task"
                    , p => p.Subject, 50);

            TaskRecurMonthlyLookupDefinition.AddVisibleColumnDefinition(
                p => p.RecurType
                , "Recur Type"
                , p => p.RecurType, 20);

            TaskRecurMonthlys.HasLookupDefinition(TaskRecurMonthlyLookupDefinition);

            TaskRecurYearlyLookupDefinition =
                new LookupDefinition<TaskRecurYearlyLookup, TlTaskRecurYearly>(TaskRecurYearlys);

            TaskRecurYearlyLookupDefinition.Include(p => p.Task)
                .AddVisibleColumnDefinition(
                    p => p.Task
                    , "Task"
                    , p => p.Subject, 50);

            TaskRecurYearlyLookupDefinition.AddVisibleColumnDefinition(
                p => p.RecurType
                , "Recur Type"
                , p => p.RecurType, 20);

            TaskRecurYearlys.HasLookupDefinition(TaskRecurYearlyLookupDefinition);

            this.TaskHistoryLookupDefinition = new LookupDefinition<TaskHistoryLookup, TlTaskHistory>(TaskHistory);

            TaskHistoryLookupDefinition.Include(p => p.Task)
                .AddVisibleColumnDefinition(p => p.Task
                    , "Task"
                    , p => p.Subject, 40);

            TaskHistoryLookupDefinition.AddVisibleColumnDefinition(
                p => p.StartDate
                , "Start Date"
                , p => p.StartDate, 20);

            TaskHistoryLookupDefinition.AddVisibleColumnDefinition(
                p => p.DueDate
                , "Due Date"
                , p => p.DueDate, 20);

            TaskHistoryLookupDefinition.AddVisibleColumnDefinition(
                p => p.CompletionDate
                , "Date Complete"
                , p => p.CompletionDate, 20);

            TaskHistory.HasLookupDefinition(TaskHistoryLookupDefinition);
        }

        protected override void SetupTemplateModel()
        {
            Tasks.HasDescription("Tasks").HasRecordDescription("Task");

            Tasks.PriorityLevel = 100;

            TaskHistory.PriorityLevel = 200;
            TaskRecurDailys.PriorityLevel = 200;
            TaskRecurWeeklys.PriorityLevel = 200;
            TaskRecurMonthlys.PriorityLevel = 200;
            TaskRecurYearlys.PriorityLevel = 200;

            TaskHistory.GetFieldDefinition(p => p.CompletionDate)
                .HasDateType(DbDateTypes.DateTime);

            TaskRecurDailys.HasDescription("Task Recur Dailys").HasRecordDescription("Recur Daily");

            TaskRecurDailys.GetFieldDefinition(p => p.TaskId)
                .DoOverrideIdentity();

            TaskRecurDailys.GetFieldDefinition(p => p.RecurType)
                .IsEnum<DailyRecurTypes>();

            TaskRecurDailys.GetFieldDefinition(p => p.TaskId)
                .DoOverrideIdentity();

            TaskRecurWeeklys.GetFieldDefinition(p => p.TaskId)
                .DoOverrideIdentity();

            TaskRecurWeeklys.GetFieldDefinition(p => p.RecurType)
                .IsEnum<WeeklyRecurTypes>();

            TaskRecurMonthlys.GetFieldDefinition(p => p.TaskId)
                .DoOverrideIdentity();

            TaskRecurMonthlys.GetFieldDefinition(p => p.RecurType)
                .IsEnum<MonthlyRecurTypes>();

            TaskRecurYearlys.GetFieldDefinition(p => p.TaskId)
                .DoOverrideIdentity();

            TaskRecurYearlys.GetFieldDefinition(p => p.RecurType)
                .IsEnum<YearlylyRecurTypes>();
        }
    }
}
