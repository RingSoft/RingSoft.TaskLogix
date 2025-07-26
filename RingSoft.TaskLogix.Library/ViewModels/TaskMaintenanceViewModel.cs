using Microsoft.EntityFrameworkCore;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.DbMaintenance;
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


        #endregion

        public ITaskMaintenanceView View { get; private set; }

        public UiCommand ReminderUiCommand { get; }

        public RelayCommand MarkCompleteCommand { get; }

        public RelayCommand RecurrenceCommand { get; }

        public TaskProcessor TaskProcessor { get; private set; }

        public TaskMaintenanceViewModel()
        {
            StatusComboBoxSetup = new TextComboBoxControlSetup();
            StatusComboBoxSetup.LoadFromEnum<TaskStatusTypes>();

            PriorityComboBoxSetup = new TextComboBoxControlSetup();
            PriorityComboBoxSetup.LoadFromEnum<TaskPriorityTypes>();

            ReminderUiCommand = new UiCommand();

            MarkCompleteCommand = new RelayCommand((() =>
            {
                ControlsGlobals.UserInterface.ShowMessageBox("Mark Complete"
                    , "Nub", RsMessageBoxIcons.Information);
            }));

            RecurrenceCommand = new RelayCommand((() =>
            {
                OnRecurrence();
            }));
            TaskProcessor = new TaskProcessor();
        }

        private void OnRecurrence()
        {
            TaskProcessor.StartDate = StartDate;
            if (DoReminder)
            {
                TaskProcessor.ReminderDateTime = ReminderDateTime;
            }

            if (View.ShowTaskRecurrenceWindow())
            {
                StartDate = TaskProcessor.StartDate;
                if (DoReminder)
                {
                    ReminderDateTime = TaskProcessor.ReminderDateTime.GetValueOrDefault();
                }
                RecordDirty = true;
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

            Notes = entity.Notes;
            TaskProcessor.LoadProcessor(entity);
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
            };
            if (DoReminder)
            {
                result.ReminderDateTime = ReminderDateTime;
            }

            TaskProcessor.SaveEntityFromTaskMaint(result);
            return result;
        }

        protected override bool SaveEntity(TlTask entity)
        {
            var result = base.SaveEntity(entity);

            if (result)
            {
                var context = SystemGlobals.DataRepository.GetDataContext();

                result = PurgeDaily(entity, context);

                if (result)
                {
                    result = PurgeWeekly(entity, context);
                }

                if (result)
                {
                    result = PurgeMonthly(entity, context);
                }

                if (result)
                {
                    result = PurgeYearly(entity, context);
                }

                if (result)
                {
                    if (TaskProcessor.ActiveRecurProcessor != null)
                        result = TaskProcessor.ActiveRecurProcessor.SaveRecurProcessor(entity, context);
                }
            }
            return result;
        }

        private static bool PurgeDaily(TlTask entity, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurDaily>()
                .FirstOrDefault(p => p.TaskId == entity.Id);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        private static bool PurgeWeekly(TlTask entity, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurWeekly>()
                .FirstOrDefault(p => p.TaskId == entity.Id);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        private static bool PurgeMonthly(TlTask entity, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurMonthly>()
                .FirstOrDefault(p => p.TaskId == entity.Id);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        private static bool PurgeYearly(TlTask entity, IDbContext context)
        {
            var result = true;
            var rec = context.GetTable<TlTaskRecurYearly>()
                .FirstOrDefault(p => p.TaskId == entity.Id);
            if (rec != null)
            {
                result = context.DeleteEntity(rec, "");
            }

            return result;
        }

        protected override void ClearData()
        {
            Id = 0;
            StartDate = DueDate = DateTime.Today;
            StatusType = TaskStatusTypes.NotStarted;
            PriorityType = TaskPriorityTypes.Normal;
            PercentComplete = 0;
            DoReminder = false;
            ReminderUiCommand.IsEnabled = false;
            ReminderDateTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 8, 0, 0);
            Notes = null;
            TaskProcessor = new TaskProcessor();
        }
    }
}
