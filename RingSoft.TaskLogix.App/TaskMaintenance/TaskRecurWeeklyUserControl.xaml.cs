using System.Windows.Controls;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurWeeklyUserControl.xaml
    /// </summary>
    public partial class TaskRecurWeeklyUserControl
    {
        public TaskRecurWeeklyUserControl()
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
                case WeeklyRecurTypes.EveryXWeeks:
                    EveryXWeeksRadio.Focus();
                    break;
                case WeeklyRecurTypes.RegenerateXWeeksAfterCompleted:
                    RegenRadio.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
