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

        public DateTime CurrentDate { get; set; } = DateTime.Today;

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

            TaskList.Clear();
            StartDate = null;
            EndDate = null;

            switch (TaskListType)
            {
                case TaskListTypes.Today:
                    Header = "Due Today";
                    EndDate = CurrentDate;
                    break;
                case TaskListTypes.Tomorrow:
                    Header = "Due Tomorrow";
                    StartDate = EndDate = GetDueTomorrowDate();
                    break;
                case TaskListTypes.ThisWeek:
                    Header = "Due This Week";
                    var startDate = GetCurrentWeekStart();
                    if (startDate == null)
                    {
                        return;
                    }

                    var endDate = GetCurrentWeekEnd();

                    if (endDate == null)
                    {
                        return; 
                    }
                    StartDate = startDate;
                    EndDate = endDate;
                    break;
                case TaskListTypes.ThisMonth:
                    Header = "Due This Month";
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

            table = table.OrderBy(p => p.DueDate);

            TaskList.Clear();
            foreach (var tlTask in table)
            {
                TaskList.Add(new TaskListItem
                {
                    TaskId = tlTask.Id,
                    Subject = tlTask.Subject,
                    DueDate = tlTask.DueDate.ToString("ddd, MMM dd, yyyy"),
                    PastDue = tlTask.DueDate < CurrentDate,
                });
            }
        }

        private DateTime GetDueTomorrowDate()
        {
            return CurrentDate.AddDays(1);
        }

        private DateTime? GetCurrentWeekStart()
        {
            var startDate = CurrentDate;
            if (startDate.DayOfWeek == DayOfWeek.Saturday)
            {
                return null;
            }

            startDate = GetDueTomorrowDate();
            if (startDate.DayOfWeek == DayOfWeek.Saturday)
            {
                return null;
            }

            return startDate.AddDays(1);
        }

        private DateTime? GetCurrentWeekEnd()
        {
            var startDate = GetCurrentWeekStart();
            if (startDate == null)
            {
                return null;
            }

            return startDate.GetValueOrDefault().AddDays(6 - (int)startDate.GetValueOrDefault().DayOfWeek);
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
