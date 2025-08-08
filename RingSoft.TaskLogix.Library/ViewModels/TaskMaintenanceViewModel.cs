using Microsoft.EntityFrameworkCore;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Lookup;
using RingSoft.DbLookup.QueryBuilder;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface ITaskMaintenanceView
    {
        bool ShowTaskRecurrenceWindow();
    }
    public class TaskMaintenanceViewModel : DbMaintenanceViewModel<TlTask>
    {
        #region Properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged(raiseDirtyFlag: false);
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
                UpdateDatesAfterStartDateChange();
                OnPropertyChanged();
            }
        }

        private TextComboBoxControlSetup _statusComboBoxSetup;

        public TextComboBoxControlSetup StatusComboBoxSetup
        {
            get { return _statusComboBoxSetup; }
            set
            {
                if (_statusComboBoxSetup == value)
                    return;

                _statusComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _statusComboBoxItem;

        public TextComboBoxItem StatusComboBoxItem
        {
            get { return _statusComboBoxItem; }
            set
            {
                if (_statusComboBoxItem == value)
                    return;

                _statusComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        private TaskStatusTypes _statusType;

        public TaskStatusTypes StatusType
        {
            get { return (TaskStatusTypes)StatusComboBoxItem.NumericValue; }
            set { StatusComboBoxItem = StatusComboBoxSetup.GetItem((byte)value); }
        }

        private DateTime _dueDate;

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                if (_dueDate == value)
                    return;

                _dueDate = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxControlSetup _priorityComboBoxSetup;

        public TextComboBoxControlSetup PriorityComboBoxSetup
        {
            get { return _priorityComboBoxSetup; }
            set
            {
                if (_priorityComboBoxSetup == value)
                    return;

                _priorityComboBoxSetup = value;
                OnPropertyChanged();
            }
        }

        private TextComboBoxItem _priorityComboBoxItem;

        public TextComboBoxItem PriorityComboBoxItem
        {
            get { return _priorityComboBoxItem; }
            set
            {
                if (_priorityComboBoxItem == value)
                    return;

                _priorityComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        private TaskPriorityTypes _priorityType;

        public TaskPriorityTypes PriorityType
        {
            get { return (TaskPriorityTypes)PriorityComboBoxItem.NumericValue; }
            set { PriorityComboBoxItem = PriorityComboBoxSetup.GetItem((byte)value); }
        }

        private double _percentComplete;

        public double PercentComplete
        {
            get { return _percentComplete; }
            set
            {
                if (_percentComplete == value)
                    return;

                _percentComplete = value;
                OnPropertyChanged();
            }
        }

        private bool _doReminder;

        public bool DoReminder
        {
            get { return _doReminder; }
            set
            {
                if (_doReminder == value)
                    return;

                _doReminder = value;

                ReminderUiCommand.IsEnabled = value;

                OnPropertyChanged();
            }
        }

        private DateTime _reminderDateTime;

        public DateTime ReminderDateTime
        {
            get { return _reminderDateTime; }
            set
            {
                if (_reminderDateTime == value)
                    return;

                _reminderDateTime = value;

                OnPropertyChanged();
            }
        }

        private string? _notes;

        public string? Notes
        {
            get { return _notes; }
            set
            {
                if (_notes == value)
                    return;

                _notes = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _snoozeDateTime;

        public DateTime? SnoozeDateTime
        {
            get { return _snoozeDateTime; }
            set
            {
                if (_snoozeDateTime == value)
                    return;

                _snoozeDateTime = value;
                OnPropertyChanged();
            }
        }

        private LookupDefinition<TaskHistoryLookup, TlTaskHistory> _historyLookup;

        public LookupDefinition<TaskHistoryLookup, TlTaskHistory> HistoryLookup
        {
            get => _historyLookup;
            set
            {
                if (_historyLookup == value)
                    return;

                _historyLookup = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ITaskMaintenanceView View { get; private set; }

        public UiCommand ReminderUiCommand { get; }

        public RelayCommand MarkCompleteCommand { get; }

        public RelayCommand RecurrenceCommand { get; }

        public UiCommand SnoozeUiCommand { get; }

        public TaskProcessor TaskProcessor { get; private set; }

        private bool _loading;

        public TaskMaintenanceViewModel()
        {
            StatusComboBoxSetup = new TextComboBoxControlSetup();
            StatusComboBoxSetup.LoadFromEnum<TaskStatusTypes>();

            PriorityComboBoxSetup = new TextComboBoxControlSetup();
            PriorityComboBoxSetup.LoadFromEnum<TaskPriorityTypes>();

            ReminderUiCommand = new UiCommand();
            SnoozeUiCommand = new UiCommand();

            MarkCompleteCommand = new RelayCommand((() =>
            {
                DoMarkComplete();
            }));

            RecurrenceCommand = new RelayCommand((() =>
            {
                OnRecurrence();
            }));
            TaskProcessor = new TaskProcessor();

            HistoryLookup = AppGlobals.LookupContext.TaskHistoryLookupDefinition.Clone();
            HistoryLookup.InitialOrderByType = OrderByTypes.Descending;
            RegisterLookup(HistoryLookup);
        }

        protected override void Initialize()
        {
            base.Initialize();

            TablesToDelete.Add(AppGlobals.LookupContext.TaskRecurDailys);
            TablesToDelete.Add(AppGlobals.LookupContext.TaskRecurWeeklys);
            TablesToDelete.Add(AppGlobals.LookupContext.TaskRecurMonthlys);
            TablesToDelete.Add(AppGlobals.LookupContext.TaskRecurYearlys);
        }

        private void UpdateDatesAfterStartDateChange()
        {
            if (!_loading)
            {
                DueDate = StartDate;
                var oldReminder = ReminderDateTime;
                ReminderDateTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day
                    , oldReminder.Hour, oldReminder.Minute, oldReminder.Second);
            }
        }

        private void OnRecurrence()
        {
            _loading = true;
            UpdateTaskProcessor();

            if (View.ShowTaskRecurrenceWindow())
            {
                UpdateAfterRecurrence();
                RecordDirty = true;
            }
            _loading = false;
        }

        private void UpdateTaskProcessor()
        {
            TaskProcessor.StartDate = StartDate;
            TaskProcessor.DueDate = DueDate;
            if (DoReminder)
            {
                TaskProcessor.ReminderDateTime = ReminderDateTime;
            }
        }

        private void UpdateAfterRecurrence()
        {
            _loading = true;
            StartDate = TaskProcessor.StartDate;
            DueDate = TaskProcessor.DueDate.GetValueOrDefault();
            if (DoReminder)
            {
                ReminderDateTime = TaskProcessor.ReminderDateTime.GetValueOrDefault();
            }

            _loading = false;
        }

        private void DoMarkComplete()
        {
            if (DoSave() == DbMaintenanceResults.Success)
            {
                _loading = true;
                TaskProcessor = TaskProcessor.LoadProcessor(Id);
                TaskProcessor.DoMarkComplete();
                TaskProcessor.SaveProcessorAfterMarkComplete(Id);
                StartDate = TaskProcessor.StartDate;
                ReminderDateTime = TaskProcessor.ReminderDateTime.GetValueOrDefault();
                DueDate = TaskProcessor.DueDate.GetValueOrDefault();
                SnoozeUiCommand.Visibility = UiVisibilityTypes.Collapsed;
                SnoozeDateTime = null;
                RecordDirty = false;
                _loading = false;
                HistoryLookup.SetCommand(GetLookupCommand((LookupCommands.Refresh)));
            }
        }

        public void Init(ITaskMaintenanceView view)
        {
            View = view;
        }

        protected override TlTask GetEntityFromDb(TlTask newEntity, PrimaryKeyValue primaryKeyValue)
        {
            var context = SystemGlobals.DataRepository.GetDataContext();
            var table = context.GetTable<TlTask>();

            var result = table
                .Include(p => p.RecurDaily)
                .Include(p => p.RecurWeekly)
                .Include(p => p.RecurMonthly)
                .Include(p => p.RecurYearly)
                .FirstOrDefault(p => p.Id == newEntity.Id);
            return result;
        }

        protected override void PopulatePrimaryKeyControls(TlTask newEntity, PrimaryKeyValue primaryKeyValue)
        {
            Id = newEntity.Id;
        }

        protected override void LoadFromEntity(TlTask entity)
        {
            _loading = true;
            StartDate = entity.StartDate;
            StatusType = (TaskStatusTypes)entity.StatusType;
            DueDate = entity.DueDate;
            PriorityType = (TaskPriorityTypes)entity.PriorityType;
            PercentComplete = entity.PercentComplete;
            if (entity.ReminderDateTime == null)
            {
                DoReminder = false;
                ReminderDateTime = StartDate;
            }
            else
            {
                DoReminder = true;
                ReminderDateTime = entity.ReminderDateTime.GetValueOrDefault();
            }

            if (entity.SnoozeDateTime == null)
            {
                SnoozeUiCommand.Visibility = UiVisibilityTypes.Collapsed;
            }
            else
            {
                SnoozeUiCommand.Visibility = UiVisibilityTypes.Visible;
            }
            SnoozeDateTime = entity.SnoozeDateTime;
            TaskProcessor.LoadProcessor(entity);
            Notes = entity.Notes;
            _loading = false;
        }

        protected override TlTask GetEntityData()
        {
            var result = new TlTask()
            {
                Id = Id,
                Subject = KeyAutoFillValue.Text,
                StartDate = StartDate,
                StatusType = (byte)StatusType,
                DueDate = DueDate,
                PriorityType = (byte)PriorityType,
                PercentComplete = PercentComplete,
                Notes = Notes,
                SnoozeDateTime = SnoozeDateTime,
            };
            if (DoReminder)
            {
                result.ReminderDateTime = ReminderDateTime;
            }
            else
            {
                result.ReminderDateTime = null;
            }


            TaskProcessor.SaveEntityFromTaskMaint(result);
            return result;
        }

        protected override bool ValidateEntity(TlTask entity)
        {
            UpdateTaskProcessor();
            if (!TaskProcessor.AdjustStartDate())
            {
                UpdateAfterRecurrence();

                entity.StartDate = StartDate;
                entity.DueDate = DueDate;
                if (DoReminder)
                {
                    entity.ReminderDateTime = ReminderDateTime;
                }

                ControlsGlobals.UserInterface.ShowMessageBox(
                    "Start Date adjusted to match recurrence pattern."
                    , "Start Date Adjusted"
                    , RsMessageBoxIcons.Information);
            }

            return base.ValidateEntity(entity);
        }

        protected override bool SaveEntity(TlTask entity)
        {
            var result = base.SaveEntity(entity);

            if (result)
            {
                var context = SystemGlobals.DataRepository.GetDataContext();

                result = PurgeDaily(entity.Id, context);

                if (result)
                {
                    result = PurgeWeekly(entity.Id, context);
                }

                if (result)
                {
                    result = PurgeMonthly(entity.Id, context);
                }

                if (result)
                {
                    result = PurgeYearly(entity.Id, context);
                }

                if (result)
                {
                    if (TaskProcessor.ActiveRecurProcessor != null)
                        result = TaskProcessor.ActiveRecurProcessor.SaveRecurProcessor(entity, context);
                }
            }
            return result;
        }

        protected override bool DeleteEntity()
        {
            var context = SystemGlobals.DataRepository.GetDataContext();

            var result = PurgeDaily(Id, context);

            if (result)
            {
                result = PurgeWeekly(Id, context);
            }

            if (result)
            {
                result = PurgeMonthly(Id, context);
            }

            if (result)
            {
                result = PurgeYearly(Id, context);
            }

            return base.DeleteEntity();
        }

        private static bool PurgeDaily(int taskId, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurDaily>()
                .FirstOrDefault(p => p.TaskId == taskId);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        private static bool PurgeWeekly(int taskId, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurWeekly>()
                .FirstOrDefault(p => p.TaskId == taskId);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        private static bool PurgeMonthly(int taskId, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurMonthly>()
                .FirstOrDefault(p => p.TaskId == taskId);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        private static bool PurgeYearly(int taskId, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurYearly>()
                .FirstOrDefault(p => p.TaskId == taskId);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        protected override void ClearData()
        {
            _loading = true;
            Id = 0;
            StartDate = DueDate = DateTime.Today;
            StatusType = TaskStatusTypes.NotStarted;
            PriorityType = TaskPriorityTypes.Normal;
            PercentComplete = 0;
            DoReminder = true;
            ReminderUiCommand.IsEnabled = true;
            ReminderDateTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 7, 0, 0);
            Notes = null;
            TaskProcessor = new TaskProcessor();
            SnoozeDateTime = null;
            SnoozeUiCommand.Visibility = UiVisibilityTypes.Collapsed;
            _loading = false;
        }
    }
}
