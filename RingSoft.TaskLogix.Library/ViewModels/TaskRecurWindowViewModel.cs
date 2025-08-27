using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface IRecurWindowView
    {
        TaskProcessor TaskProcessor { get; set; }
        void UpdateRecurType();

        void CloseWindow(bool result);
    }
    public class TaskRecurWindowViewModel : INotifyPropertyChanged
    {
        private TaskRecurTypes _recurType;

        public TaskRecurTypes RecurType
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
                View.UpdateRecurType();
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate == value)
                    return;

                _startDate = value;
                OnPropertyChanged();
            }
        }

        private TaskRecurEndingTypes _endingType;

        public TaskRecurEndingTypes EndingType
        {
            get { return _endingType; }
            set
            {
                if (_endingType == value)
                {
                    return;
                }
                _endingType = value;
                SetEnabled();
                OnPropertyChanged();
            }
        }

        private DateTime _recurEndDate;

        public DateTime RecurEndDate
        {
            get => _recurEndDate;
            set
            {
                if (_recurEndDate == value)
                    return;

                _recurEndDate = value;
                OnPropertyChanged();
            }
        }

        private int _endAfterOccurrences;
        public int EndAfterOccurrences
        {
            get => _endAfterOccurrences;
            set
            {
                if (_endAfterOccurrences == value)
                    return;

                _endAfterOccurrences = value;
                OnPropertyChanged();
            }
        }

        public IRecurWindowView View { get; private set; }

        public TaskRecurViewModelBase ActiveRecurViewModel { get; set; }

        public RelayCommand OkCommand { get; }

        public RelayCommand CancelCommand { get; }

        public RelayCommand RemoveRecurrenceCommand { get; }

        public UiCommand EndAfterUiCommand { get; }

        public UiCommand EndByUiCommand { get; }

        public TaskRecurWindowViewModel()
        {
            OkCommand = new RelayCommand(OnOk);
            CancelCommand = new RelayCommand(OnCancel);
            RemoveRecurrenceCommand = new RelayCommand(OnRemoveRecurrence);

            EndAfterUiCommand = new UiCommand();
            EndByUiCommand = new UiCommand();
        }

        public void Init(IRecurWindowView view)
        {
            View = view;

            var recurType = view.TaskProcessor.RecurType;
            if (recurType == TaskRecurTypes.None)
            {
                recurType = TaskRecurTypes.Weekly;
            }
            RecurType = recurType;
            StartDate = view.TaskProcessor.StartDate;
            EndingType = view.TaskProcessor.RecurEndType;
            RecurEndDate = view.TaskProcessor.RecurEndDate.GetValueOrDefault();
            EndAfterOccurrences = view.TaskProcessor.EndAfterOccurrences.GetValueOrDefault();

            if (view.TaskProcessor.RecurType != TaskRecurTypes.None)
            {
                if (ActiveRecurViewModel != null)
                {
                    ActiveRecurViewModel.LoadFromTaskProcessor(view.TaskProcessor);
                }
            }
        }

        public void SetEnabled()
        {
            EndAfterUiCommand.IsEnabled = false;
            EndByUiCommand.IsEnabled = false;

            switch (EndingType)
            {
                case TaskRecurEndingTypes.NoEndDate:
                    break;
                case TaskRecurEndingTypes.EndBy:
                    EndByUiCommand.IsEnabled = true;
                    break;
                case TaskRecurEndingTypes.EndAfterOccurXTimes:
                    EndAfterUiCommand.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnOk()
        {
            View.TaskProcessor.RecurType = RecurType;
            View.TaskProcessor.StartDate = StartDate;
            View.TaskProcessor.RecurEndType = EndingType;
            View.TaskProcessor.RecurEndDate = RecurEndDate;
            View.TaskProcessor.EndAfterOccurrences = EndAfterOccurrences;
            
            if (ActiveRecurViewModel != null)
            {
                ActiveRecurViewModel.SaveToTaskProcessor(View.TaskProcessor);
            }
            View.TaskProcessor.AdjustStartDate();

            View.CloseWindow(true);
        }

        private void OnCancel()
        {
            View.CloseWindow(false);
        }

        private void OnRemoveRecurrence()
        {
            View.TaskProcessor.RecurType = TaskRecurTypes.None;
            View.CloseWindow(true);
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
