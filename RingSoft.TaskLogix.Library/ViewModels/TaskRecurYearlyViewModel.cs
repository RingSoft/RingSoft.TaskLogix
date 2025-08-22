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
                SetEnabled();
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
            get { return (WeekTypes)WeekTypeComboBoxItem.NumericValue; }
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

        private TextComboBoxControlSetup _weekMonthTypeComboBoxSetup;

        public TextComboBoxControlSetup WeekMonthTypeComboBoxSetup
        {
            get { return _weekMonthTypeComboBoxSetup; }
            set
            {
                if (_weekMonthTypeComboBoxSetup == value)
                    return;

                _weekMonthTypeComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _weekMonthTypeComboBoxItem;

        public TextComboBoxItem WeekMonthTypeComboBoxItem
        {
            get { return _weekMonthTypeComboBoxItem; }
            set
            {
                if (_weekMonthTypeComboBoxItem == value)
                    return;

                _weekMonthTypeComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public MonthsInYear WeekMonthType
        {
            get { return (MonthsInYear)WeekMonthTypeComboBoxItem.NumericValue; }
            set { WeekMonthTypeComboBoxItem = WeekMonthTypeComboBoxSetup.GetItem((int)value); }
        }

        private int _regenYearsAfterCompleted;

        public int RegenYearsAfterCompleted
        {
            get { return _regenYearsAfterCompleted; }
            set
            {
                if (_regenYearsAfterCompleted == value)
                    return;

                _regenYearsAfterCompleted = value;
                OnPropertyChanged();
            }
        }


        public UiCommand EveryMonthTypeUiCommand { get; }

        public UiCommand MonthDayUiCommand { get; }

        public UiCommand WeekTypeUiCommand { get; }

        public UiCommand DayTypeUiCommand { get; }

        public UiCommand WeekMonthTypeUiCommand { get; }

        public UiCommand RegenYearsAfterCompletedUiCommand { get; }


        public TaskRecurYearlyViewModel()
        {
            EveryMonthTypeUiCommand = new UiCommand();
            MonthDayUiCommand = new UiCommand();
            WeekTypeUiCommand = new UiCommand();
            DayTypeUiCommand = new UiCommand();
            WeekMonthTypeUiCommand = new UiCommand();
            RegenYearsAfterCompletedUiCommand =new UiCommand();
            EveryMonthTypeComboBoxSetup = new TextComboBoxControlSetup();
            EveryMonthTypeComboBoxSetup.LoadFromEnum<MonthsInYear>();
            
            WeekTypeComboBoxSetup = new TextComboBoxControlSetup();
            WeekTypeComboBoxSetup.LoadFromEnum<WeekTypes>();

            DayTypeComboBoxSetup = new TextComboBoxControlSetup();
            DayTypeComboBoxSetup.LoadFromEnum<DayTypes>();

            WeekMonthTypeComboBoxSetup = new TextComboBoxControlSetup();
            WeekMonthTypeComboBoxSetup.LoadFromEnum<MonthsInYear>();

            EveryMonthType = MonthsInYear.January;
            MonthDay = 1;
            this.WeekType = WeekTypes.First;
            this.DayType = DayTypes.Day;
            WeekMonthType = MonthsInYear.January;
            RegenYearsAfterCompleted = 1;
        }

        public void SetEnabled()
        {
            EveryMonthTypeUiCommand.IsEnabled = false;
            MonthDayUiCommand.IsEnabled = false;
            WeekTypeUiCommand.IsEnabled = false;
            DayTypeUiCommand.IsEnabled = false;
            WeekMonthTypeUiCommand.IsEnabled = false;
            RegenYearsAfterCompletedUiCommand.IsEnabled = false;

            switch (RecurType)
            {
                case YearlylyRecurTypes.EveryMonthDayX:
                    EveryMonthTypeUiCommand.IsEnabled = true;
                    MonthDayUiCommand.IsEnabled = true;
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    WeekTypeUiCommand.IsEnabled = true;
                    DayTypeUiCommand.IsEnabled = true;
                    WeekMonthTypeUiCommand.IsEnabled = true;
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    RegenYearsAfterCompletedUiCommand.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void SetInitialValues()
        {
            
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            RecurType = taskProcessor.YearlyProcessor.RecurType;
            EveryMonthType = taskProcessor.YearlyProcessor.EveryMonthType;
            MonthDay = taskProcessor.YearlyProcessor.MonthDay;
            WeekType = taskProcessor.YearlyProcessor.WeekType;
            DayType = taskProcessor.YearlyProcessor.DayType;
            WeekMonthType = taskProcessor.YearlyProcessor.WeekMonthType;
            RegenYearsAfterCompleted = taskProcessor.YearlyProcessor.RegenYearsAfterCompleted;
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            taskProcessor.YearlyProcessor.RecurType = RecurType;
            taskProcessor.YearlyProcessor.EveryMonthType = EveryMonthType;
            taskProcessor.YearlyProcessor.MonthDay = MonthDay;
            taskProcessor.YearlyProcessor.WeekType = WeekType;
            taskProcessor.YearlyProcessor.DayType = DayType;
            taskProcessor.YearlyProcessor.WeekMonthType = WeekMonthType;
            taskProcessor.YearlyProcessor.RegenYearsAfterCompleted = RegenYearsAfterCompleted;
        }
    }
}
