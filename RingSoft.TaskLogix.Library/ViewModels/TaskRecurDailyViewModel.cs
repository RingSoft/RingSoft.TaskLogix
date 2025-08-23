using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskRecurDailyViewModel : TaskRecurViewModelBase
    {
        private DailyRecurTypes _recurType;

        public DailyRecurTypes RecurType
        {
            get { return _recurType; }
            set
            {
                if (_recurType == value)
                {
                    return;
                }
                _recurType = value;
                SetEnabled();
                OnPropertyChanged();
            }
        }

        private int _recurDays;

        public int RecurDays
        {
            get { return _recurDays; }
            set
            {
                if (_recurDays == value)
                    return;

                _recurDays = value;
                OnPropertyChanged();
            }
        }

        private int _regenDaysAfterCompleted;

        public int RegenDaysAfterCompleted
        {
            get { return _regenDaysAfterCompleted; }
            set
            {
                if (_regenDaysAfterCompleted == value)
                    return;

                _regenDaysAfterCompleted = value;
                OnPropertyChanged();
            }
        }

        public UiCommand RecurDaysUiCommand { get; }

        public UiCommand RegenDaysUiCommand { get; }

        public TaskRecurDailyViewModel()
        {
            RecurDaysUiCommand = new UiCommand();
            RegenDaysUiCommand = new UiCommand();
        }

        public override TaskRecurTypes GeTaskRecurType()
        {
            return TaskRecurTypes.Daily;
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            RecurType = taskProcessor.DailyProcessor.RecurType;
            RecurDays = taskProcessor.DailyProcessor.RecurDays;
            RegenDaysAfterCompleted = taskProcessor.DailyProcessor.RegenDaysAfterCompleted;
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            taskProcessor.DailyProcessor.RecurType = RecurType;
            taskProcessor.DailyProcessor.RecurDays = RecurDays;
            taskProcessor.DailyProcessor.RegenDaysAfterCompleted = RegenDaysAfterCompleted;
        }

        public void SetEnabled()
        {
            RecurDaysUiCommand.IsEnabled = false;
            RegenDaysUiCommand.IsEnabled = false;

            switch (RecurType)
            {
                case DailyRecurTypes.EveryXDays:
                    RecurDaysUiCommand.IsEnabled = true;
                    break;
                case DailyRecurTypes.EveryWeekday:
                    break;
                case DailyRecurTypes.RegenerateXDaysAfterCompleted:
                    RegenDaysUiCommand.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
