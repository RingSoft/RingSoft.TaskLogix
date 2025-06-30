﻿using RingSoft.DbLookup;
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
    }
}
