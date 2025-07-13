﻿using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
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

        public UiCommand ReminderUiCommand { get; }

        public TaskMaintenanceViewModel()
        {
            StatusComboBoxSetup = new TextComboBoxControlSetup();
            StatusComboBoxSetup.LoadFromEnum<TaskStatusTypes>();

            PriorityComboBoxSetup = new TextComboBoxControlSetup();
            PriorityComboBoxSetup.LoadFromEnum<TaskPriorityTypes>();

            ReminderUiCommand = new UiCommand();
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
        }
    }
}
