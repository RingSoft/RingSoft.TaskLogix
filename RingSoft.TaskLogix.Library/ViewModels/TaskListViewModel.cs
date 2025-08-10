using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public enum TaskListTypes
    {
        Today = 0,
        Tomorrow = 1,
        ThisWeek = 2,
        ThisMonth = 3,
        NextMonth = 4,
        Older = 5,
    }

    public class TaskListItem
    {
        public int TaskId { get; set; }

        public string Subject { get; set; }

        public string DueDate { get; set; }

        public bool PastDue { get; set; }
    }

    public class TaskListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskListItem> _taskList;

        public ObservableCollection<TaskListItem> TaskList
        {
            get { return _taskList; }
            set
            {
                if (_taskList == value)
                {
                    return;
                }

                _taskList = value;
                OnPropertyChanged();
            }
        }

        private TaskListItem _selectedItem;

        public TaskListItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                    return;

                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                if (_header == value)
                    return;

                _header = value;
                OnPropertyChanged();
            }
        }

        public TaskListTypes TaskListType { get; private set; }

        public DateTime? StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public TaskListViewModel()
        {
            TaskList = new ObservableCollection<TaskListItem>();
        }

        public void Initialize(TaskListTypes taskListType)
        {
            TaskListType = taskListType;

            StartDate = null;
            EndDate = DateTime.Today;

            switch (TaskListType)
            {
                case TaskListTypes.Today:
                    Header = "Due Today";
                    break;
                case TaskListTypes.Tomorrow:
                    Header = "Due Tomorrow";
                    StartDate = DateTime.Today.AddDays(1);
                    EndDate = DateTime.Today.AddDays(1);
                    break;
                case TaskListTypes.ThisWeek:
                    break;
                case TaskListTypes.ThisMonth:
                    break;
                case TaskListTypes.NextMonth:
                    break;
                case TaskListTypes.Older:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var context = SystemGlobals.DataRepository.GetDataContext();
            var table = context.GetTable<TlTask>();
            if (StartDate.HasValue)
            {
                table = table.Where(p => p.DueDate >= StartDate);
            }

            if (EndDate.HasValue)
            {
                table = table.Where(p => p.DueDate <= EndDate);
            }

            TaskList.Clear();
            foreach (var tlTask in table)
            {
                TaskList.Add(new TaskListItem
                {
                    TaskId = tlTask.Id,
                    Subject = tlTask.Subject,
                    DueDate = tlTask.DueDate.ToString("ddd, MMM dd, yyyy"),
                    PastDue = tlTask.DueDate < DateTime.Today,
                });
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
