using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurMonthlyProcessor : TaskRecurProcessorBase
    {
        public MonthlyRecurTypes RecurType { get; set; }

        public int DayXOfEvery { get; set; }

        public int OfEveryYMonths { get; set; }

        public WeekTypes WeekType { get; set; }

        public DayTypes DayType { get; set; }

        public int OfEveryWeekTypeMonths { get; set; }

        public int RegenMonthsAfterCompleted { get; set; }

        public TaskRecurMonthlyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
        }

        public override void DoMarkComplete()
        {
            switch (RecurType)
            {
                case MonthlyRecurTypes.DayXOfEveryYMonths:
                    TaskProcessor.StartDate = GetDayXOfEvery(TaskProcessor.StartDate, OfEveryYMonths);
                    break;
                case MonthlyRecurTypes.XthWeekdayOfEveryYMonths:
                    TaskProcessor.StartDate = GetNthWeekdayOfEveryMonth(TaskProcessor.StartDate, OfEveryWeekTypeMonths);
                    break;
                case MonthlyRecurTypes.RegenerateXMonthsAfterCompleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AdjustStartDate()
        {
            throw new NotImplementedException();
        }

        private DateTime GetDayXOfEvery(DateTime startDate, int addMonths)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            startDate = startDate.AddMonths(addMonths);
            var lastDayOfMonth = startDate.GetLastDayOfMonth();
            var newDay = DayXOfEvery;

            if (DayXOfEvery > lastDayOfMonth)
            {
                newDay = lastDayOfMonth;
            }

            return new DateTime(startDate.Year, startDate.Month, newDay);
        }

        internal DateTime GetNthWeekdayOfEveryMonth(DateTime startDate, int addMonths)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            startDate = startDate.AddMonths(addMonths);
            var result = startDate;
            switch (WeekType)
            {
                case WeekTypes.First:
                case WeekTypes.Second:
                case WeekTypes.Third:
                case WeekTypes.Fourth:
                    switch (DayType)
                    {
                        case DayTypes.Day:
                            result = GetNthDayTypeDay(startDate);
                            break;
                        case DayTypes.Weekday:
                            result = GetNthWeekTypeWeekDay(startDate);
                            break;
                        case DayTypes.WeekendDay:
                            result = GetNthWeekTypeWeekendDay(startDate);
                            break;
                        case DayTypes.Sunday:
                        case DayTypes.Monday:
                        case DayTypes.Tuesday:
                        case DayTypes.Wednesday:
                        case DayTypes.Thursday:
                        case DayTypes.Friday:
                        case DayTypes.Saturday:
                            result = GetNthWeekDayDay(startDate);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case WeekTypes.Last:
                    switch (DayType)
                    {
                        case DayTypes.Day:
                            result = new DateTime(startDate.Year, startDate.Month, startDate.GetLastDayOfMonth());
                            break;
                        case DayTypes.Weekday:
                            result = GetLastWeekTypeWeekDay(startDate);
                            break;
                        case DayTypes.WeekendDay:
                            result = GetLastWeeklyTypeWeekendDay(startDate);
                            break;
                        case DayTypes.Sunday:
                        case DayTypes.Monday:
                        case DayTypes.Tuesday:
                        case DayTypes.Wednesday:
                        case DayTypes.Thursday:
                        case DayTypes.Friday:
                        case DayTypes.Saturday:
                            result = GetLastWeekDayDay(startDate);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        private DateTime GetLastWeekTypeWeekDay(DateTime startDate)
        {
            throw new Exception();
        }

        private DateTime GetLastWeeklyTypeWeekendDay(DateTime startDate)
        {
            throw new Exception();
        }

        private DateTime GetLastWeekDayDay(DateTime startDate)
        {
            throw new Exception();
        }

        private DateTime GetNthDayTypeDay(DateTime startDate)
        {
            throw new Exception();
        }
        private DateTime GetNthWeekDayDay(DateTime startDate)
        {
            throw new Exception();
        }

        private DateTime GetNthWeekTypeWeekDay(DateTime startDate)
        {
            throw new Exception();
        }

        private DateTime GetNthWeekTypeWeekendDay(DateTime startDate)
        {
            throw new Exception();
        }
    }
}
