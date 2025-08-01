using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskRecurYearlyViewModel : TaskRecurViewModelBase
    {
        private YearlylyRecurTypes _recurType;

        public YearlylyRecurTypes RecurType
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
            }
        }

        private TextComboBoxControlSetup _everyMonthTypeComboBoxSetup;

        public TextComboBoxControlSetup EveryMonthTypeComboBoxSetup
        {
            get { return _everyMonthTypeComboBoxSetup; }
            set
            {
                if (_everyMonthTypeComboBoxSetup == value)
                    return;

                _everyMonthTypeComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _everyMonthTypeComboBoxItem;

        public TextComboBoxItem EveryMonthTypeComboBoxItem
        {
            get { return _everyMonthTypeComboBoxItem; }
            set
            {
                if (_everyMonthTypeComboBoxItem == value)
                    return;

                _everyMonthTypeComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public MonthsInYear EveryMonthType
        {
            get { return (MonthsInYear)EveryMonthTypeComboBoxItem.NumericValue; }
            set { EveryMonthTypeComboBoxItem = EveryMonthTypeComboBoxSetup.GetItem((int)value); }
        }

        private int _monthDay;

        public int MonthDay
        {
            get { return _monthDay; }
            set
            {
                if (_monthDay == value)
                    return;

                _monthDay = value;
                OnPropertyChanged();
            }
        }


        public UiCommand EveryMonthTypeUiCommand { get; }

        public UiCommand MonthDayUiCommand { get; }

        public TaskRecurYearlyViewModel()
        {
            EveryMonthTypeUiCommand = new UiCommand();
            MonthDayUiCommand = new UiCommand();

            EveryMonthTypeComboBoxSetup = new TextComboBoxControlSetup();
            EveryMonthTypeComboBoxSetup.LoadFromEnum<MonthsInYear>();
            EveryMonthType = MonthsInYear.January;

            MonthDay = 1;
        }

        public void SetEnabled()
        {
            //EveryMonthTypeUiCommand.IsEnabled = false;
            //MonthDayUiCommand.IsEnabled = false;
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }
    }
}
