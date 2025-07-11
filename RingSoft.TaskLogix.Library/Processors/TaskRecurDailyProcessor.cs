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

            return taskProcessor.WeeklyProcessor.GetNextDate(startDate);
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
