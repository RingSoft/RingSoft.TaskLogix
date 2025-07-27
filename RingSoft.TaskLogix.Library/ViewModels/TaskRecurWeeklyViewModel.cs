using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskRecurWeeklyViewModel : TaskRecurViewModelBase
    {
        private WeeklyRecurTypes _recurType;

        public WeeklyRecurTypes RecurType
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

        private int _recurWeeks;

        public int RecurWeeks
        {
            get { return _recurWeeks; }
            set
            {
                if (_recurWeeks == value)
                    return;

                _recurWeeks = value;
                OnPropertyChanged();
            }
        }

        private int _regenWeeksAfterCompleted;

        public int RegenWeeksAfterCompleted
        {
            get { return _regenWeeksAfterCompleted; }
            set
            {
                if (_regenWeeksAfterCompleted == value)
                    return;

                _regenWeeksAfterCompleted = value;
                OnPropertyChanged();
            }
        }

        public UiCommand RecurWeeksUiCommand { get; }

        public UiCommand RegenWeeksUiCommand { get; set; }

        public TaskRecurWeeklyViewModel()
        {
            RecurWeeksUiCommand = new UiCommand();
            RegenWeeksUiCommand = new UiCommand();

            RecurType = WeeklyRecurTypes.EveryXWeeks;
            RecurWeeks = 1;
            RegenWeeksAfterCompleted = 1;

        }

        public void SetEnabled()
        {

        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }
    }
}
