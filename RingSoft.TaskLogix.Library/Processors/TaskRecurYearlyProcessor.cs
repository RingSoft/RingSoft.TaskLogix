using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurYearlyProcessor : TaskRecurProcessorBase
    {
        public YearlylyRecurTypes RecurType { get; set; }

        public MonthsInYear EveryMonthType { get; set; }

        public int MonthDay { get; set; }

        public WeekTypes WeekType { get; set; }

        public DayTypes DayType { get; set; }

        public MonthsInYear WeekMonthType { get; set; }

        public int RegenYearsAfterCompleted { get; set; }

        public TaskRecurYearlyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
        }

        public override void DoMarkComplete()
        {
            switch (RecurType)
            {
                case YearlylyRecurTypes.EveryMonthDayX:
                    TaskProcessor.StartDate = GetDayXOfEvery(TaskProcessor.StartDate);
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AdjustStartDate()
        {
            switch (RecurType)
            {
                case YearlylyRecurTypes.EveryMonthDayX:
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private DateTime GetDayXOfEvery(DateTime startDate, int addYears = 1)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            startDate = startDate.AddYears(addYears);
            var lastDayOfMonth = startDate.GetLastDayOfMonth();
            var newDay = MonthDay;

            if (MonthDay > lastDayOfMonth)
            {
                newDay = lastDayOfMonth;
            }

            return new DateTime(startDate.Year, startDate.Month, newDay);
        }
    }
}
