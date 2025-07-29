using System.Windows.Controls;
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
        }

        public override TaskRecurViewModelBase GetRecurViewModel()
        {
            return LocalViewModel;
        }

        public override void SetInitialFocus()
        {
            
        }
    }
}
