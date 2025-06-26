﻿using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurWeeklyProcessor : TaskRecurProcessorBase
    {
        public WeeklyRecurTypes RecurType { get; set; }

        public int RecurWeeks { get; set; }

        public bool Sunday { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public int RegenWeeksAfterCompleted { get; set; }

        public TaskRecurWeeklyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
            RecurType = WeeklyRecurTypes.EveryXWeeks;
            RecurWeeks = 1;
            switch (TaskProcessor.StartDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Sunday = true;
                    break;
                case DayOfWeek.Monday:
                    Monday = true;
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday = true;
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday = true;
                    break;
                case DayOfWeek.Thursday:
                    Thursday = true;
                    break;
                case DayOfWeek.Friday:
                    Friday = true;
                    break;
                case DayOfWeek.Saturday:
                    Saturday = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetPropsFromEntity(TlTaskRecurWeekly entity)
        {
            throw new NotImplementedException();
        }

        public override bool DoMarkComplete()
        {
            TaskProcessor.StartDate = GetNextDate(TaskProcessor.StartDate);
            return true;
        }

        private DateTime GetNextDate(DateTime startDate)
        {
            var daysToAdd = GetDaysToAdd(startDate);

            return startDate.AddDays(daysToAdd);
        }

        private int GetDaysToAdd(DateTime startDate)
        {
            var nextWeekday = GetNextWeekDay(startDate);
            var result = 0;
            if (nextWeekday <= (int)startDate.DayOfWeek)
            {
                result = (int)startDate.DayOfWeek + (7 - nextWeekday);
            }
            else
            {
                result = nextWeekday - (int)startDate.DayOfWeek;
            }
            return result;
        }

        private int GetNextWeekDay(DateTime startDate)
        {
            var startIndex = (int)startDate.DayOfWeek + 1;

            var selList = new List<bool>();
            selList.Add(Sunday);
            selList.Add(Monday);
            selList.Add(Tuesday);
            selList.Add(Wednesday);
            selList.Add(Thursday);
            selList.Add(Friday);
            selList.Add(Saturday);

            var result = selList.FindIndex(startIndex, p => p.Equals(true));

            if (result == -1)
            {
                result = selList.FindIndex(0, p => p.Equals(true));
            }
            return result;
        }
    }
}
