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


        public IRecurWindowView View { get; private set; }

        public TaskRecurViewModelBase ActiveRecurViewModel { get; set; }

        public RelayCommand OkCommand { get; }

        public RelayCommand CancelCommand { get; }

        public RelayCommand RemoveRecurrenceCommand { get; }

        public TaskRecurWindowViewModel()
        {
            OkCommand = new RelayCommand(OnOk);
            CancelCommand = new RelayCommand(OnCancel);
            RemoveRecurrenceCommand = new RelayCommand(OnRemoveRecurrence);
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

            if (view.TaskProcessor.RecurType != TaskRecurTypes.None)
            {
                if (ActiveRecurViewModel != null)
                {
                    ActiveRecurViewModel.LoadFromTaskProcessor(view.TaskProcessor);
                }
            }
        }

        private void OnOk()
        {
            View.TaskProcessor.RecurType = RecurType;
            View.TaskProcessor.StartDate = StartDate;
            
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
            ControlsGlobals.UserInterface.ShowMessageBox("Remove Recurrence", "Nub", RsMessageBoxIcons.Information);
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
