using RingSoft.DataEntryControls.Engine;
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

        public TaskMaintenanceViewModel()
        {
            StatusComboBoxSetup = new TextComboBoxControlSetup();
            StatusComboBoxSetup.LoadFromEnum<TaskStatusTypes>();
        }

        #endregion
        protected override void PopulatePrimaryKeyControls(TlTask newEntity, PrimaryKeyValue primaryKeyValue)
        {
            Id = newEntity.Id;
        }

        protected override void LoadFromEntity(TlTask entity)
        {
            StartDate = entity.StartDate;
            StatusType = (TaskStatusTypes)entity.StatusType;
        }

        protected override TlTask GetEntityData()
        {
            return new TlTask()
            {
                Id = Id,
                Subject = KeyAutoFillValue.Text,
                StartDate = StartDate,
                StatusType = (byte)StatusType,
            };
        }

        protected override void ClearData()
        {
            Id = 0;
            StartDate = DateTime.Today;
            StatusType = TaskStatusTypes.NotStarted;
        }
    }
}
