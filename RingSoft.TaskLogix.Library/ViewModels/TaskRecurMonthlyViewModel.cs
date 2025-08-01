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

        private int _ofEveryWeekTypeMonths;

        public int OfEveryWeekTypeMonths
        {
            get { return _ofEveryWeekTypeMonths; }
            set
            {
                if (_ofEveryWeekTypeMonths == value)
                    return;

                _ofEveryWeekTypeMonths = value;
                OnPropertyChanged();
            }
        }

        private int _regenMonthsAfterCompleted;

        public int RegenMonthsAfterCompleted
        {
            get { return _regenMonthsAfterCompleted; }
            set
            {
                if (_regenMonthsAfterCompleted == value)
                    return;

                _regenMonthsAfterCompleted = value;
                OnPropertyChanged();
            }
        }


        public UiCommand DayXOfEveryUiCommand { get; }

        public UiCommand OfEveryYMonthsUiCommand { get; }

        public UiCommand WeekTypeUiCommand { get; }

        public UiCommand DayTypeUiCommand { get; }

        public UiCommand OfEveryWeekTypeMonthsUiCommand { get; }

        public UiCommand RegenMonthsAfterCompletedUiCommand { get; }

        public TaskRecurMonthlyViewModel()
        {
            DayXOfEveryUiCommand = new UiCommand();
            OfEveryYMonthsUiCommand = new UiCommand();
            WeekTypeUiCommand = new UiCommand();
            DayTypeUiCommand = new UiCommand();
            OfEveryWeekTypeMonthsUiCommand = new UiCommand();
            RegenMonthsAfterCompletedUiCommand = new UiCommand();

            WeekTypeComboBoxSetup = new TextComboBoxControlSetup();
            WeekTypeComboBoxSetup.LoadFromEnum<WeekTypes>();

            DayTypeComboBoxSetup = new TextComboBoxControlSetup();
            DayTypeComboBoxSetup.LoadFromEnum<DayTypes>();

            this.RecurType = MonthlyRecurTypes.DayXOfEveryYMonths;
            this.DayXOfEvery = 1;
            this.OfEveryYMonths = 1;
            this.WeekType = WeekTypes.First;
            this.DayType = DayTypes.Day;
            this.OfEveryWeekTypeMonths = 1;
            this.RegenMonthsAfterCompleted = 1;
        }

        public void SetEnabled()
        {
            DayXOfEveryUiCommand.IsEnabled = false;
            OfEveryYMonthsUiCommand.IsEnabled = false;
            WeekTypeUiCommand.IsEnabled = false;
            DayTypeUiCommand.IsEnabled = false;
            OfEveryWeekTypeMonthsUiCommand.IsEnabled = false;
            RegenMonthsAfterCompletedUiCommand.IsEnabled = false;

            switch (RecurType)
            {
                case MonthlyRecurTypes.DayXOfEveryYMonths:
                    DayXOfEveryUiCommand.IsEnabled = true;
                    OfEveryYMonthsUiCommand.IsEnabled = true;
                    break;
                case MonthlyRecurTypes.XthWeekdayOfEveryYMonths:
                    WeekTypeUiCommand.IsEnabled = true;
                    DayTypeUiCommand.IsEnabled = true;
                    OfEveryWeekTypeMonthsUiCommand.IsEnabled = true;
                    break;
                case MonthlyRecurTypes.RegenerateXMonthsAfterCompleted:
                    RegenMonthsAfterCompletedUiCommand.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            this.RecurType = taskProcessor.MonthlyProcessor.RecurType;
            this.DayXOfEvery = taskProcessor.MonthlyProcessor.DayXOfEvery;
            this.OfEveryYMonths = taskProcessor.MonthlyProcessor.OfEveryYMonths;
            this.WeekType = taskProcessor.MonthlyProcessor.WeekType;
            this.DayType = taskProcessor.MonthlyProcessor.DayType;
            this.OfEveryWeekTypeMonths = taskProcessor.MonthlyProcessor.OfEveryWeekTypeMonths;
            this.RegenMonthsAfterCompleted = taskProcessor.MonthlyProcessor.RegenMonthsAfterCompleted;
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            taskProcessor.MonthlyProcessor.RecurType = this.RecurType;
            taskProcessor.MonthlyProcessor.DayXOfEvery = this.DayXOfEvery;
            taskProcessor.MonthlyProcessor.OfEveryYMonths = this.OfEveryYMonths;
            taskProcessor.MonthlyProcessor.WeekType = this.WeekType;
            taskProcessor.MonthlyProcessor.DayType = this.DayType;
            taskProcessor.MonthlyProcessor.OfEveryWeekTypeMonths = this.OfEveryWeekTypeMonths;
            taskProcessor.MonthlyProcessor.RegenMonthsAfterCompleted = this.RegenMonthsAfterCompleted;
        }
    }
}
