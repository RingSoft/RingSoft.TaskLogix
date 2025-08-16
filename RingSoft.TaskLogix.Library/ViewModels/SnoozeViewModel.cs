using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public enum SnoozeTypes
    {
        TimeBlock = 0,
        DateTime = 1,
    }

    public enum SnoozeTimeBlocks
    {
        [Description("Second(s)")]
        Seconds = 0,
        [Description("Minute(s)")]
        Minutes = 1,
        [Description("Hour(s)")]
        Hours = 2,
        [Description("Day(s)")]
        Days = 3,
        [Description("Week(s)")]
        Weeks = 4,
        [Description("Month(s)")]
        Months = 5,
        [Description("Year(s)")]
        Years = 6,
    }
    public interface ISnoozeView
    {
        void Close();
    }

    public class SnoozeViewModel : INotifyPropertyChanged
    {
        private DateTime _snoozeDateTime;

        public DateTime SnoozeDateTime
        {
            get { return _snoozeDateTime; }
            set
            {
                if (_snoozeDateTime == value)
                {
                    return;
                }
                _snoozeDateTime = value;
                OnPropertyChanged();
            }
        }

        private SnoozeTypes _snoozeType;

        public SnoozeTypes SnoozeType
        {
            get { return _snoozeType; }
            set
            {
                if (_snoozeType == value)
                    return;

                _snoozeType = value;

                UpdateUi();

                OnPropertyChanged();
            }
        }

        private int _timeBlockValue;

        public int TimeBlockValue
        {
            get { return _timeBlockValue; }
            set
            {
                if (_timeBlockValue == value)
                    return;

                _timeBlockValue = value;

                OnPropertyChanged();
            }
        }


        private TextComboBoxControlSetup _timeTypeComboBoxSetup;

        public TextComboBoxControlSetup TimeTypeComboBoxSetup
        {
            get { return _timeTypeComboBoxSetup; }
            set
            {
                if (_timeTypeComboBoxSetup == value)
                    return;

                _timeTypeComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _timeTypeComboBoxItem;

        public TextComboBoxItem TimeTypeComboBoxItem
        {
            get { return _timeTypeComboBoxItem; }
            set
            {
                if (_timeTypeComboBoxItem == value)
                    return;

                _timeTypeComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public SnoozeTimeBlocks TimeType
        {
            get { return (SnoozeTimeBlocks)TimeTypeComboBoxItem.NumericValue; }
            set { _timeTypeComboBoxItem = TimeTypeComboBoxSetup.GetItem((int)value); }
        }


        public ISnoozeView View { get; private set; }

        public bool DialogResult { get; private set; }

        public RelayCommand OkCommand { get; }

        public RelayCommand CancelCommand { get; }

        public UiCommand DateTimeUiCommand { get; }

        public UiCommand TimeBlockUiCommand { get; }
        public UiCommand TimeTypeComboUiCommand { get; }

        private TlTask _task;

        public SnoozeViewModel()
        {
            DateTimeUiCommand = new UiCommand();
            TimeBlockUiCommand = new UiCommand();
            TimeTypeComboUiCommand = new UiCommand();

            TimeTypeComboBoxSetup = new TextComboBoxControlSetup();
            TimeTypeComboBoxSetup.LoadFromEnum<SnoozeTimeBlocks>();

            SnoozeType = SnoozeTypes.TimeBlock;
            TimeType = SnoozeTimeBlocks.Minutes;
            TimeBlockValue = 15;

            OkCommand = new RelayCommand(OnOK);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public void Init(ISnoozeView view, TlTask task)
        {
            View = view;
            _task = task;

            var snoozeDate = _task.SnoozeDateTime;
            if (snoozeDate == null)
            {
                snoozeDate = DateTime.Today;
            }

            snoozeDate = snoozeDate.Value.AddDays(1);
            snoozeDate = new DateTime(snoozeDate.Value.Year, snoozeDate.Value.Month, snoozeDate.Value.Day, 7, 0, 0);

            SnoozeDateTime = snoozeDate.Value;
        }

        private void OnOK()
        {
            _task.SnoozeDateTime = SnoozeDateTime;
            DialogResult = true;
            View.Close();
        }

        private void OnCancel()
        {
            View.Close();
        }

        public void UpdateUi()
        {
            DateTimeUiCommand.IsEnabled = false;
            TimeTypeComboUiCommand.IsEnabled = false;
            TimeBlockUiCommand.IsEnabled = false;

            switch (SnoozeType)
            {
                case SnoozeTypes.TimeBlock:
                    TimeBlockUiCommand.IsEnabled = true;
                    TimeTypeComboUiCommand.IsEnabled = true;
                    break;
                case SnoozeTypes.DateTime:
                    DateTimeUiCommand.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
