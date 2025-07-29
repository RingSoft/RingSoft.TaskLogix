using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskRecurMonthlyViewModel : TaskRecurViewModelBase
    {
        private MonthlyRecurTypes _recurType;

        public MonthlyRecurTypes RecurType
        {
            get { return _recurType; }
            set
            {
                if (_recurType == value)
                {
                    return;
                }
                _recurType = value;
                OnPropertyChanged();
                SetEnabled();
            }
        }

        private int _dayXOfEvery;

        public int DayXOfEvery
        {
            get { return _dayXOfEvery; }
            set
            {
                if (_dayXOfEvery == value)
                {
                    return;
                }
                _dayXOfEvery = value;
                OnPropertyChanged();
            }
        }

        private int _ofEveryYMonths;

        public int OfEveryYMonths
        {
            get { return _ofEveryYMonths; }
            set
            {
                if (_ofEveryYMonths == value)
                    return;

                _ofEveryYMonths = value;
                OnPropertyChanged();
            }
        }

        public UiCommand DayXOfEveryUiCommand { get; }

        public UiCommand OfEveryYMonthsUiCommand { get; }

        public TaskRecurMonthlyViewModel()
        {
            DayXOfEveryUiCommand = new UiCommand();

            OfEveryYMonthsUiCommand = new UiCommand();
        }

        public void SetEnabled()
        {
            DayXOfEveryUiCommand.IsEnabled = false;
            OfEveryYMonthsUiCommand.IsEnabled = false;
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }
    }
}
