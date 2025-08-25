using System.Windows.Controls;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurYearlyUserControl.xaml
    /// </summary>
    public partial class TaskRecurYearlyUserControl
    {
        public TaskRecurYearlyUserControl()
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
                case YearlylyRecurTypes.EveryMonthDayX:
                    EveryMonthTypeRadio.Focus();
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    NthWeekdayTypeOfMonthRadio.Focus();
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    RegenRadio.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
