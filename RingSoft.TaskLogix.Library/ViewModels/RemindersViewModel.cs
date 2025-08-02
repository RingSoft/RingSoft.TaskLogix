using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface IReminderView
    {
        void CloseWindow();

        void ResetSelection();

        bool ProcessSnooze(TlTask tlTask);
    };

    public class RemindersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reminder> _reminders;

        public ObservableCollection<Reminder> Reminders
        {
            get { return _reminders; }
            set
            {
                if (_reminders == value)
                {
                    return;
                }
                _reminders = value;
                OnPropertyChanged();
            }
        }

        private Reminder _selectedReminder;

        public Reminder SelectedReminder
        {
            get { return _selectedReminder; }
            set
            {
                if (_selectedReminder == value)
                    return;

                _selectedReminder = value;
                OnPropertyChanged();
            }
        }


        public IReminderView View { get; private set; }

        public RelayCommand OpenTaskCommand { get; }

        public RelayCommand MarkTaskCompleteCommand { get; }

        public RelayCommand SnoozeTaskCommand { get; }

        public RemindersViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();

            OpenTaskCommand = new RelayCommand(OpenTask);
            MarkTaskCompleteCommand = new RelayCommand(MarkTaskComplete);
            SnoozeTaskCommand = new RelayCommand(SnoozeTask);
        }

        public void Initialize(IReminderView view, List<Reminder> remindersList)
        {
            View = view;
            ProcessNewReminders(remindersList);
        }

        public void ProcessNewReminders(List<Reminder> remindersList)
        {
            Reminders.Clear();

            foreach (var reminder in remindersList)
            {
                Reminders.Add(reminder);
            }

            if (Reminders.Any())
            {
                View.ResetSelection();
            }
            else
            {
                View.CloseWindow();
            }
        }

        private void OpenTask()
        {
            var tlTask = new TlTask
            {
                Id = SelectedReminder.TaskId,
            };
            var primaryKey = AppGlobals.LookupContext.Tasks.GetPrimaryKeyValueFromEntity(tlTask);
            if (primaryKey != null)
            {
                SystemGlobals.TableRegistry.ShowEditAddOnTheFly(primaryKey);
                AppGlobals.MainViewModel.HandleReminders();
            }
        }

        private void MarkTaskComplete()
        {
            var taskProcessor = TaskProcessor.LoadProcessor(SelectedReminder.TaskId);
            if (taskProcessor != null)
            {
                taskProcessor.DoMarkComplete();
                if (taskProcessor.SaveProcessorAfterMarkComplete(SelectedReminder.TaskId))
                {
                    AppGlobals.MainViewModel.HandleReminders();
                }
            }
        }

        private void SnoozeTask()
        {
            var context = SystemGlobals.DataRepository.GetDataContext();
            var tlTask = context.GetTable<TlTask>().FirstOrDefault(p => p.Id == SelectedReminder.TaskId);

            if (tlTask != null)
            {
                if (View.ProcessSnooze(tlTask))
                {
                    if (context.SaveEntity(tlTask, ""))
                    {
                        AppGlobals.MainViewModel.HandleReminders();
                    }
                }
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
