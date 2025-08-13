using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.DataEntryControls.Engine;
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

    public interface ITaskListView
    {
        void ShowMaintenanceTab(PrimaryKeyValue primaryKey = null);
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

        public RelayCommand OpenTaskCommand { get; }

        public ITaskListView View { get; private set; }

        public TaskListViewModel()
        {
            TaskList = new ObservableCollection<TaskListItem>();

            OpenTaskCommand = new RelayCommand(OpenTask);
        }

        public void Initialize(TaskListTypes taskListType, ITaskListView view = null)
        {
            TaskListType = taskListType;
            View = view;

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
                    StartDate = GetCurrentMonthStart();
                    EndDate = GetCurrentMonthEnd();
                    break;
                case TaskListTypes.NextMonth:
                    Header = "Due Next Month";
                    StartDate = GetNextMonthStart();
                    EndDate = GetNextMonthEnd();
                    break;
                case TaskListTypes.Older:
                    Header = "Older";
                    StartDate = GetNextMonthEnd().AddDays(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var context = SystemGlobals.DataRepository.GetDataContext();
            var table = context.GetTable<TlTask>();
            var doQuery = false;
            if (StartDate.HasValue)
            {
                table = table.Where(p => p.DueDate >= StartDate);
                doQuery = true;
            }

            if (EndDate.HasValue)
            {
                table = table.Where(p => p.DueDate <= EndDate);
                doQuery = true;
            }

            table = table.OrderBy(p => p.DueDate);

            TaskList.Clear();
            if (doQuery)
            {
                foreach (var tlTask in table)
                {
                    var addTask = true;
                    if (tlTask.RecurType == (byte)TaskRecurTypes.None)
                    {
                        if (tlTask.StatusType == (byte)TaskStatusTypes.Completed)
                        {
                            addTask = false;
                        }
                    }

                    if (addTask)
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

        private DateTime? GetCurrentMonthStart()
        {
            var startDate = GetCurrentWeekEnd();

            if (startDate == null)
            {
                startDate = GetDueTomorrowDate();
            }

            startDate = startDate.GetValueOrDefault().AddDays(1);

            if (startDate.GetValueOrDefault().Month > CurrentDate.Month)
            {
                return null;
            }
            return startDate;
        }

        private DateTime? GetCurrentMonthEnd()
        {
            var startDate = GetCurrentMonthStart();

            if (startDate == null)
            {
                return null;
            }

            return new DateTime(startDate.GetValueOrDefault().Year
                , startDate.GetValueOrDefault().Month
                , startDate.GetValueOrDefault().GetLastDayOfMonth());
        }

        public DateTime GetNextMonthStart()
        {
            var date = new DateTime(CurrentDate.Year, CurrentDate.Month, 1).AddMonths(1);
            var weekEndDate = GetCurrentWeekEnd();
            if (weekEndDate == null)
            {
                var tomorrowDate = GetDueTomorrowDate();
                if (tomorrowDate == date)
                {
                    date = tomorrowDate.AddDays(1);
                }
            }
            else
            {
                if (weekEndDate > date)
                {
                    date = weekEndDate.GetValueOrDefault().AddDays(1);
                }
            }

            return date;
        }

        public DateTime GetNextMonthEnd()
        {
            var date = new DateTime(CurrentDate.Year, CurrentDate.Month, 1).AddMonths(1);
            date = new DateTime(date.Year, date.Month, date.GetLastDayOfMonth());
            return date;
        }

        private void OpenTask()
        {
            var tlTask = new TlTask
            {
                Id = SelectedItem.TaskId,
            };

            var primaryKey = AppGlobals.LookupContext.Tasks.GetPrimaryKeyValueFromEntity(tlTask);

            if (primaryKey != null)
            {
                if (View != null)
                {
                    View.ShowMaintenanceTab(primaryKey);
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
