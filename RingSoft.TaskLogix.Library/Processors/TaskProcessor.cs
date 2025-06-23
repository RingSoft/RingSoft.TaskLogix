using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskProcessor
    {
        public TaskRecurProcessorBase ActiveRecurProcessor { get; private set; }

        public TaskRecurWeeklyProcessor WeeklyProcessor { get; private set; }

        public int Id { get; set; }

        public string Subject { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReminderDateTime { get; set; }

        public DateTime? SnoozeDateTime { get; set; }

        public TaskStatusTypes StatusType { get; set; }

        public TaskPriorityTypes PriorityType { get; set; }

        public double PercentComplete { get; set; }

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

        public DateTime? RecurStartDate { get; set; }

        public TaskRecurEndingTypes RecurEndType { get; set; }

        public DateTime? RecurEndDate { get; set; }

        public int? EndAfterOccurrences { get; set; }

        public TaskProcessor()
        {
            RecurType = TaskRecurTypes.None;
        }

        public void DoMarkComplete()
        {
            if (ActiveRecurProcessor != null)
            {
                ActiveRecurProcessor.DoMarkComplete();
            }
        }

        public bool DoSave()
        {
            var context = SystemGlobals.DataRepository.GetDataContext();
            var task = GetEntityData();
            if (!context.SaveEntity(task, "Saving Task"))
            {
                return false;
            }

            var primaryKey = AppGlobals.LookupContext.Tasks.GetPrimaryKeyValueFromEntity(task);
            if (primaryKey != null)
            {
                if (!primaryKey.CreateRecordLock())
                {
                    return false;
                }
            }

            if (WeeklyProcessor != null)
            {

            }

            return true;
        }

        private TlTask GetEntityData()
        {
            var result = new TlTask()
            {

            };
            return result;
        }
    }
}
