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

        private TextComboBoxControlSetup _weekTypeComboBoxSetup;

        public TextComboBoxControlSetup WeekTypeComboBoxSetup
        {
            get { return _weekTypeComboBoxSetup; }
            set
            {
                if (_weekTypeComboBoxSetup == value)
                    return;

                _weekTypeComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _weekTypeComboBoxItem;

        public TextComboBoxItem WeekTypeComboBoxItem
        {
            get { return _weekTypeComboBoxItem; }
            set
            {
                if (_weekTypeComboBoxItem == value)
                    return;

                _weekTypeComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public WeekTypes WeekType
        {
            get { return (WeekTypes)WeekTypeComboBoxItem.NumericValue;}
            set { WeekTypeComboBoxItem = WeekTypeComboBoxSetup.GetItem((int)value); }
        }

        private TextComboBoxControlSetup _dayTypeComboBoxSetup;

        public TextComboBoxControlSetup DayTypeComboBoxSetup
        {
            get { return _dayTypeComboBoxSetup; }
            set
            {
                if (_dayTypeComboBoxSetup == value)
                    return;

                _dayTypeComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _dayTypeComboBoxItem;

        public TextComboBoxItem DayTypeComboBoxItem
        {
            get { return _dayTypeComboBoxItem; }
            set
            {
                if (_dayTypeComboBoxItem == value)
                    return;

                _dayTypeComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public DayTypes DayType
        {
            get { return (DayTypes)DayTypeComboBoxItem.NumericValue; }
            set { DayTypeComboBoxItem = DayTypeComboBoxSetup.GetItem((int)value); }
        }

        public UiCommand DayXOfEveryUiCommand { get; }

        public UiCommand OfEveryYMonthsUiCommand { get; }

        public UiCommand WeekTypeUiCommand { get; }

        public UiCommand DayTypeUiCommand { get; }

        public TaskRecurMonthlyViewModel()
        {
            DayXOfEveryUiCommand = new UiCommand();
            OfEveryYMonthsUiCommand = new UiCommand();
            WeekTypeUiCommand = new UiCommand();
            DayTypeUiCommand = new UiCommand();

            WeekTypeComboBoxSetup = new TextComboBoxControlSetup();
            WeekTypeComboBoxSetup.LoadFromEnum<WeekTypes>();

            DayTypeComboBoxSetup = new TextComboBoxControlSetup();
            DayTypeComboBoxSetup.LoadFromEnum<DayTypes>();
        }

        public void SetEnabled()
        {
            //DayXOfEveryUiCommand.IsEnabled = false;
            //OfEveryYMonthsUiCommand.IsEnabled = false;
            //WeekTypeUiCommand.IsEnabled = false;
            //DayTypeUiCommand.IsEnabled = false;
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }
    }
}
