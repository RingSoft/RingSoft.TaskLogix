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
        }

        protected override TlTaskHistory GetEntityData()
        {
            throw new NotImplementedException();
        }

        protected override void ClearData()
        {
            Id = 0;
            TaskAutoFillValue = null;
        }
    }
}
