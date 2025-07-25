using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class TaskRecurDailyViewModel : TaskRecurViewModelBase
    {
        private DailyRecurTypes _recurType;

        public DailyRecurTypes RecurType
        {
            get { return _recurType; }
            set
            {
                if (_recurType == value)
                {
                    return;
                }
                _recurType = value;
                OnPropertyChanged();
            }
        }

        private int _recurDays;

        public int RecurDays
        {
            get { return _recurDays; }
            set
            {
                if (_recurDays == value)
                    return;

                _recurDays = value;
                OnPropertyChanged();
            }
        }

        private int _regenDaysAfterCompleted;

        public int RegenDaysAfterCompleted
        {
            get { return _regenDaysAfterCompleted; }
            set
            {
                if (_regenDaysAfterCompleted == value)
                    return;

                _regenDaysAfterCompleted = value;
                OnPropertyChanged();
            }
        }



        public TaskRecurDailyViewModel()
        {
            RecurType = DailyRecurTypes.EveryWeekday;
            RecurDays = 1;
            RegenDaysAfterCompleted = 1;
        }

        public override void LoadFromTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }

        public override void SaveToTaskProcessor(TaskProcessor taskProcessor)
        {
            
        }
    }
}
