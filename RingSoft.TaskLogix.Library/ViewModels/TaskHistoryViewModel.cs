using RingSoft.DbLookup;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskHistoryViewModel : DbMaintenanceViewModel<TlTaskHistory>
    {
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
            
        }

        protected override TlTaskHistory GetEntityData()
        {
            throw new NotImplementedException();
        }

        protected override void ClearData()
        {
            Id = 0;
        }
    }
}
