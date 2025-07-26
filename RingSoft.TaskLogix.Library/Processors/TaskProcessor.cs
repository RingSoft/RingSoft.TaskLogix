﻿using Microsoft.EntityFrameworkCore;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskProcessor
    {
        public TaskRecurProcessorBase ActiveRecurProcessor { get; private set; }

        public TaskRecurDailyProcessor DailyProcessor { get; private set; }

        public TaskRecurWeeklyProcessor WeeklyProcessor { get; private set; }

        public TaskRecurMonthlyProcessor MonthlyProcessor { get; private set; }

        public TaskRecurYearlyProcessor YearlyProcessor { get; private set; }

        public int TaskId { get; private set; }

        public DateTime StartDate { get; set; }

        public DateTime? ReminderDateTime { get; set; }

        private TaskRecurTypes _recurType;
        public TaskRecurTypes RecurType
        {
            get => _recurType;
            set
            {
                if (_recurType == value)
                {
                    return;
                }
                _recurType = value;

                DailyProcessor = null;
                WeeklyProcessor = null;
                MonthlyProcessor = null;
                YearlyProcessor = null;

                ActiveRecurProcessor = null;

                switch (_recurType)
                {
                    case TaskRecurTypes.None:
                        ActiveRecurProcessor = null;
                        break;
                    case TaskRecurTypes.Daily:
                        DailyProcessor = new TaskRecurDailyProcessor(this);
                        ActiveRecurProcessor = DailyProcessor;
                        break;
                    case TaskRecurTypes.Weekly:
                        WeeklyProcessor = new TaskRecurWeeklyProcessor(this);
                        ActiveRecurProcessor = WeeklyProcessor;
                        break;
                    case TaskRecurTypes.Monthly:
                        MonthlyProcessor = new TaskRecurMonthlyProcessor(this);
                        ActiveRecurProcessor = MonthlyProcessor;
                        break;
                    case TaskRecurTypes.Yearly:
                        YearlyProcessor = new TaskRecurYearlyProcessor(this);
                        ActiveRecurProcessor = YearlyProcessor;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public TaskRecurEndingTypes RecurEndType { get; set; }

        public DateTime? RecurEndDate { get; set; }

        public int? EndAfterOccurrences { get; set; }

        public TaskProcessor()
        {
            RecurType = TaskRecurTypes.None;
        }

        public void DoMarkComplete()
        {
            var origStartDate = StartDate;
            if (ActiveRecurProcessor != null)
            {
                ActiveRecurProcessor.DoMarkComplete();
            }
            AdjustReminderDate(origStartDate);
        }

        public void AdjustStartDate()
        {
            var origStartDate = StartDate;
            if (ActiveRecurProcessor != null)
            {
                ActiveRecurProcessor.AdjustStartDate();
            }
            AdjustReminderDate(origStartDate);
        }

        public void SetPropsFromEntity(TlTask task)
        {

        }

        public bool SaveProcessor(int taskId)
        {
            var result = true;

            var context = SystemGlobals.DataRepository.GetDataContext();

            var task = context.GetTable<TlTask>()
                .FirstOrDefault(p => p.Id == taskId);

            if (task == null)
                task = new TlTask();

            SaveEntity(task);

            result = context.SaveEntity(task, "Saving Task");
            if (!result)
            {
                return result;
            }
            TaskId = task.Id;

            var tlRecurDailyRec = context.GetTable<TlTaskRecurDaily>()
                .FirstOrDefault(p => p.TaskId == TaskId);
            if (tlRecurDailyRec != null)
            {
                result = context.DeleteEntity(tlRecurDailyRec, "Deleting Existing Daily Recurring");
                if (!result)
                {
                    return result;
                }
            }

            switch (RecurType)
            {
                case TaskRecurTypes.None:
                    break;
                case TaskRecurTypes.Daily:
                    var tlTaskRecurDaily = new TlTaskRecurDaily
                    {
                        TaskId = TaskId,
                    };
                    DailyProcessor.SaveEntity(tlTaskRecurDaily);
                    result = context.SaveEntity(tlTaskRecurDaily, "Saving Task Daily Recurring");
                    if (!result)
                    {
                        return result;
                    }
                    break;
                case TaskRecurTypes.Weekly:
                    break;
                case TaskRecurTypes.Monthly:
                    break;
                case TaskRecurTypes.Yearly:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public void SaveEntityFromTaskMaint(TlTask tlTask)
        {
            SaveProcessorProps(tlTask);
        }
        public void SaveEntity(TlTask tlTask)
        {
            tlTask.StartDate = StartDate;
            tlTask.ReminderDateTime = ReminderDateTime;
            SaveProcessorProps(tlTask);
        }

        private void SaveProcessorProps(TlTask tlTask)
        {
            tlTask.RecurType = (byte)RecurType;
            tlTask.RecurEndType = (byte)RecurEndType;
            tlTask.RecurEndDate = null;
            tlTask.EndAfterOccurrences = null;

            switch (RecurEndType)
            {
                case TaskRecurEndingTypes.NoEndDate:
                    break;
                case TaskRecurEndingTypes.EndBy:
                    tlTask.RecurEndDate = RecurEndDate;
                    break;
                case TaskRecurEndingTypes.EndAfterOccurXTimes:
                    tlTask.EndAfterOccurrences = EndAfterOccurrences;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static TaskProcessor LoadProcessor(int taskId)
        {
            var result = new TaskProcessor();
            var context = SystemGlobals.DataRepository.GetDataContext();

            var tlTask = context.GetTable<TlTask>()
                .Include(p => p.RecurDaily)
                .Include(p => p.RecurWeekly)
                .Include(p => p.RecurMonthly)
                .Include(p => p.RecurYearly)
                .FirstOrDefault(p => p.Id == taskId);

            result.LoadProcessor(tlTask);
            return result;
        }

        public void LoadProcessor(TlTask task)
        {
            TaskId = task.Id;
            RecurType = (TaskRecurTypes)task.RecurType;
            this.StartDate = task.StartDate;
            this.ReminderDateTime = task.ReminderDateTime;
            
            this.RecurEndType = (TaskRecurEndingTypes)task.RecurEndType;
            switch (this.RecurEndType)
            {
                case TaskRecurEndingTypes.NoEndDate:
                    break;
                case TaskRecurEndingTypes.EndBy:
                    this.RecurEndDate = task.RecurEndDate;
                    break;
                case TaskRecurEndingTypes.EndAfterOccurXTimes:
                    this.EndAfterOccurrences = task.EndAfterOccurrences;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (ActiveRecurProcessor != null) 
                ActiveRecurProcessor.LoadRecurProcessor(task);
        }

        public void AdjustReminderDate(DateTime origStartDate)
        {
            if (ReminderDateTime != null)
            {
                var dateDif = origStartDate - ReminderDateTime;
                var ticksDif = dateDif.GetValueOrDefault().Ticks;
                ReminderDateTime = StartDate.AddTicks(-ticksDif);
            }
        }
    }
}
