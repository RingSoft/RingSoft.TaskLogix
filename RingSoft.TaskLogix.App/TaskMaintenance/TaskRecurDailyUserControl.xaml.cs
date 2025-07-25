using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurDailyUserControl.xaml
    /// </summary>
    public partial class TaskRecurDailyUserControl
    {
        public TaskRecurDailyUserControl()
        {
            InitializeComponent();
        }

        public override TaskRecurViewModelBase GetRecurViewModel()
        {
            return LocalViewModel;
        }

        public override void SetInitialFocus()
        {
            switch (LocalViewModel.RecurType)
            {
                case DailyRecurTypes.EveryXDays:
                    EveryXDaysRadio.Focus();
                    break;
                case DailyRecurTypes.EveryWeekday:
                    EveryWeekdayRadio.Focus();
                    break;
                case DailyRecurTypes.RegenerateXDaysAfterCompleted:
                    RegenRadio.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
