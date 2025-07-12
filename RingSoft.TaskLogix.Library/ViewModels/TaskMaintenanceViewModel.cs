using RingSoft.DbLookup;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskMaintenanceViewModel : DbMaintenanceViewModel<TlTask>
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (Id == value)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged(raiseDirtyFlag:false);
            }
        }

        protected override void PopulatePrimaryKeyControls(TlTask newEntity, PrimaryKeyValue primaryKeyValue)
        {
            Id = newEntity.Id;
        }

        protected override void LoadFromEntity(TlTask entity)
        {
            
        }

        protected override TlTask GetEntityData()
        {
            return new TlTask();
        }

        protected override void ClearData()
        {
            Id = 0;
        }
    }
}
