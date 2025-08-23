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

        private bool _sun;

        public bool Sun
        {
            get { return _sun; }
            set
            {
                if (_sun == value)
                {
                    return;
                }
                _sun = value;
                OnPropertyChanged();
            }
        }

        private bool _mon;

        public bool Mon
        {
            get { return _mon; }
            set
            {
                if (_mon == value)
                    return;

                _mon = value;
                OnPropertyChanged();
            }
        }

        private bool _tue;

        public bool Tue
        {
            get { return _tue; }
            set
            {
                if (_tue == value)
                    return;

                _tue = value;
                OnPropertyChanged();
            }
        }

        private bool _wed;

        public bool Wed
        {
            get { return _wed; }
            set
            {
                if (_wed == value)
                    return;

                _wed = value;
                OnPropertyChanged();
            }
        }

        private bool _thu;

        public bool Thu
        {
            get { return _thu; }
            set
            {
                if (_thu == value)
                    return;

                _thu = value;
                OnPropertyChanged();
            }
        }

        private bool _fri;

        public bool Fri
        {
            get { return _fri; }
            set
            {
                if (_fri == value)  
                    return;

                _fri = value;
                OnPropertyChanged();
            }
        }

        private bool _sat;

        public bool Sat
        {
            get { return _sat; }
            set
            {
                if (_sat == value)
                    return;

                _sat = value;
                OnPropertyChanged();
            }
        }


        public UiCommand RecurWeeksUiCommand { get; }

        public UiCommand RegenWeeksUiCommand { get; }

        public UiCommand SunUiCommand { get; }

        public UiCommand MonUiCommand { get; }

        public UiCommand TueUiCommand { get; }

        public UiCommand WedUiCommand { get; }

        public UiCommand ThuUiCommand { get; }

        public UiCommand FriUiCommand { get; }

        public UiCommand SatUiCommand { get; }

        public TaskRecurWeeklyViewModel()
        {
            RecurWeeksUiCommand = new UiCommand();
            RegenWeeksUiCommand = new UiCommand();
            SunUiCommand = new UiCommand();
            MonUiCommand = new UiCommand();
            TueUiCommand = new UiCommand();
            WedUiCommand = new UiCommand();
            ThuUiCommand = new UiCommand();
            FriUiCommand = new UiCommand();
            SatUiCommand = new UiCommand();
        }

        public void SetEnabled()
        {
            RecurWeeksUiCommand.IsEnabled = false;
            RegenWeeksUiCommand.IsEnabled = false;
            SunUiCommand.IsEnabled = false;
            MonUiCommand.IsEnabled = false;
            TueUiCommand.IsEnabled = false;
            WedUiCommand.IsEnabled = false;
            ThuUiCommand.IsEnabled = false;
            FriUiCommand.IsEnabled = false;
            SatUiCommand.IsEnabled = false;

            switch (RecurType)
            {
                case WeeklyRecurTypes.EveryXWeeks:
                    RecurWeeksUiCommand.IsEnabled = true;
                    SunUiCommand.IsEnabled = true;
                    MonUiCommand.IsEnabled = true;
                    TueUiCommand.IsEnabled = true;
                    WedUiCommand.IsEnabled = true;
                    ThuUiCommand.IsEnabled = true;
                    FriUiCommand.IsEnabled = true;
                    SatUiCommand.IsEnabled = true;
                    break;
                case WeeklyRecurTypes.RegenerateXWeeksAfterCompleted:
                    RegenWeeksUiCommand.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override TaskRecurTypes GeTaskRecurType()
        {
            return TaskRecurTypes.Weekly;
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            this.RecurType = taskProcessor.WeeklyProcessor.RecurType;
            this.RecurWeeks = taskProcessor.WeeklyProcessor.RecurWeeks;
            this.RegenWeeksAfterCompleted = taskProcessor.WeeklyProcessor.RegenWeeksAfterCompleted;

            this.Sun = taskProcessor.WeeklyProcessor.Sunday;
            this.Mon = taskProcessor.WeeklyProcessor.Monday;
            this.Tue = taskProcessor.WeeklyProcessor.Tuesday;
            this.Wed = taskProcessor.WeeklyProcessor.Wednesday;
            this.Thu = taskProcessor.WeeklyProcessor.Thursday;
            this.Fri = taskProcessor.WeeklyProcessor.Friday;
            this.Sat = taskProcessor.WeeklyProcessor.Saturday;
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            taskProcessor.WeeklyProcessor.RecurType = this.RecurType;
            taskProcessor.WeeklyProcessor.RecurWeeks = this.RecurWeeks;
            taskProcessor.WeeklyProcessor.RegenWeeksAfterCompleted = this.RegenWeeksAfterCompleted;

            taskProcessor.WeeklyProcessor.Sunday = this.Sun;
            taskProcessor.WeeklyProcessor.Monday = this.Mon;
            taskProcessor.WeeklyProcessor.Tuesday = this.Tue;
            taskProcessor.WeeklyProcessor.Wednesday = this.Wed;
            taskProcessor.WeeklyProcessor.Thursday = this.Thu;
            taskProcessor.WeeklyProcessor.Friday = this.Fri;
            taskProcessor.WeeklyProcessor.Saturday = this.Sat;
        }
    }
}
