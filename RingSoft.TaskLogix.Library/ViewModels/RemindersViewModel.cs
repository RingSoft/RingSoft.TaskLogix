using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface IReminderView
    {

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

        public RemindersViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();
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
