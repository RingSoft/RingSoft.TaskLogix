using RingSoft.DbLookup;
using RingSoft.DbLookup.AutoFill;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskHistoryViewModel : DbMaintenanceViewModel<TlTaskHistory>
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
                OnPropertyChanged();
            }
        }

        private AutoFillSetup _taskAutoFillSetup;

        public AutoFillSetup TaskAutoFillSetup
        {
            get { return _taskAutoFillSetup; }
            set
            {
                if (_taskAutoFillSetup == value)
                    return;

                _taskAutoFillSetup = value;
                OnPropertyChanged();
            }
        }

        private AutoFillValue _taskAutoFillValue;

        public AutoFillValue TaskAutoFillValue
        {
            get { return _taskAutoFillValue; }
            set
            {
                if (_taskAutoFillValue == value)
                    return;

                _taskAutoFillValue = value;
                OnPropertyChanged();
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

        private DateTime _completionDate;

        public DateTime CompletionDate
        {
            get { return _completionDate; }
            set
            {
                if (_completionDate == value)
                    return;

                _completionDate = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public TaskHistoryViewModel()
        {
            TaskAutoFillSetup = new AutoFillSetup(TableDefinition.GetFieldDefinition(p => p.TaskId));
        }

        protected override void Initialize()
        {
            ReadOnlyMode = true;
            base.Initialize();
        }

        protected override void PopulatePrimaryKeyControls(TlTaskHistory newEntity, PrimaryKeyValue primaryKeyValue)
        {
            Id = newEntity.Id;
        }

        protected override void LoadFromEntity(TlTaskHistory entity)
        {
            TaskAutoFillValue = entity.Task.GetAutoFillValue();
            StartDate = entity.StartDate;
            DueDate = entity.DueDate;
            CompletionDate = entity.CompletionDate;
        }

        protected override TlTaskHistory GetEntityData()
        {
            throw new NotImplementedException();
        }

        protected override void ClearData()
        {
            Id = 0;
            TaskAutoFillValue = null;
            StartDate = DueDate = CompletionDate = DateTime.MinValue;
        }
    }
}
