using System.Windows.Controls;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurMonthlyUserControl.xaml
    /// </summary>
    public partial class TaskRecurMonthlyUserControl
    {
        public TaskRecurMonthlyUserControl()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                LocalViewModel.SetEnabled();
            };
        }

        public override TaskRecurViewModelBase GetRecurViewModel()
        {
            return LocalViewModel;
        }

        public override void SetInitialFocus()
        {
            switch (LocalViewModel.RecurType)
            {
                case MonthlyRecurTypes.DayXOfEveryYMonths:
                    DayXOfEveryYMonthsRadio.Focus();
                    break;
                case MonthlyRecurTypes.XthWeekdayOfEveryYMonths:
                    XthWeekdayOfEveryYMonthsRadio.Focus();
                    break;
                case MonthlyRecurTypes.RegenerateXMonthsAfterCompleted:
                    RegenRadio.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
