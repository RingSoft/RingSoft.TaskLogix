using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurDailyProcessor : TaskRecurProcessorBase
    {
        public DailyRecurTypes RecurType { get; set; }

        public int RecurDays { get; set; }

        public int RegenDaysAfterCompleted { get; set; }

        public TaskRecurDailyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
            RecurDays = 1;
            RegenDaysAfterCompleted = 1;
        }

        public override void DoMarkComplete()
        {
            switch (RecurType)
            {
                case DailyRecurTypes.EveryXDays:
                    TaskProcessor.StartDate = TaskProcessor.StartDate.AddDays(RecurDays);
                    break;
                case DailyRecurTypes.EveryWeekday:
                    TaskProcessor.StartDate = GetNextWeekdayDate(TaskProcessor.StartDate);
                    break;
                case DailyRecurTypes.RegenerateXDaysAfterCompleted:
                    var daysToAdd = RegenDaysAfterCompleted;
                    TaskProcessor.StartDate = DateTime.Today.AddDays(daysToAdd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AdjustStartDate()
        {
            switch (RecurType)
            {
                case DailyRecurTypes.EveryWeekday:
                    TaskProcessor.StartDate = GetNextWeekdayDate(TaskProcessor.StartDate.AddDays(-1));
                    break;
            }
        }

        public override void LoadRecurProcessor(TlTask task)
        {
            if (task.RecurDaily.Any())
            {
                var taskRecurDaily = task.RecurDaily.FirstOrDefault();
                if (taskRecurDaily != null)
                {
                    this.RecurType = (DailyRecurTypes)taskRecurDaily.RecurType;
                    this.RecurDays = taskRecurDaily.RecurDays.GetValueOrDefault();
                    this.RegenDaysAfterCompleted = taskRecurDaily.RegenDaysAfterCompleted.GetValueOrDefault();
                }
            }
        }

        public override bool SaveRecurProcessor(TlTask task, IDbContext context)
        {
            var tlTaskRecurDaily = new TlTaskRecurDaily()
            {
                TaskId = task.Id,
                RecurType = (byte)RecurType,
                RecurDays = RecurDays,
                RegenDaysAfterCompleted = RegenDaysAfterCompleted,
            };

            return context.AddSaveEntity(tlTaskRecurDaily, "");
        }

        public override string GetRecurText()
        {
            var text = string.Empty;

            switch (RecurType)
            {
                case DailyRecurTypes.EveryXDays:
                    text = $"Every {RecurDays} Day(s)";
                    break;
                case DailyRecurTypes.EveryWeekday:
                    text = $"Every Weekday";
                    break;
                case DailyRecurTypes.RegenerateXDaysAfterCompleted:
                    text = $"Every {RegenDaysAfterCompleted} Day(s) After the Task Has Been Completed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return text;
        }

        private DateTime GetNextWeekdayDate(DateTime startDate)
        {
            var taskProcessor = new TaskProcessor();
            taskProcessor.StartDate = startDate;
            taskProcessor.RecurType = TaskRecurTypes.Weekly;

            taskProcessor.WeeklyProcessor.RecurType = WeeklyRecurTypes.EveryXWeeks;
            taskProcessor.WeeklyProcessor.Sunday = false;
            taskProcessor.WeeklyProcessor.Monday = true;
            taskProcessor.WeeklyProcessor.Tuesday = true;
            taskProcessor.WeeklyProcessor.Wednesday = true;
            taskProcessor.WeeklyProcessor.Thursday = true;
            taskProcessor.WeeklyProcessor.Friday = true;
            taskProcessor.WeeklyProcessor.Saturday = false;

            return taskProcessor.WeeklyProcessor.GetNextDate(startDate, false);
        }

        public void SaveEntity(TlTaskRecurDaily entity)
        {
            entity.RecurType = (byte)RecurType;
            switch (RecurType)
            {
                case DailyRecurTypes.EveryXDays:
                    entity.RecurDays = RecurDays;
                    break;
                case DailyRecurTypes.EveryWeekday:
                    break;
                case DailyRecurTypes.RegenerateXDaysAfterCompleted:
                    entity.RegenDaysAfterCompleted = RegenDaysAfterCompleted;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
