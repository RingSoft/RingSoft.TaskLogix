using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskProcessor
    {
        public TaskRecurProcessorBase ActiveRecurProcessor { get; private set; }

        public TaskRecurWeeklyProcessor WeeklyProcessor { get; private set; }

        public int TaskId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? ReminderDateTime { get; set; }

        public DateTime? SnoozeDateTime { get; set; }

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

                WeeklyProcessor = null;
                ActiveRecurProcessor = null;

                switch (_recurType)
                {
                    case TaskRecurTypes.None:
                        ActiveRecurProcessor = null;
                        break;
                    case TaskRecurTypes.Daily:
                        break;
                    case TaskRecurTypes.Weekly:
                        WeeklyProcessor = new TaskRecurWeeklyProcessor(this);
                        ActiveRecurProcessor = WeeklyProcessor;
                        break;
                    case TaskRecurTypes.Monthly:
                        break;
                    case TaskRecurTypes.Yearly:
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
