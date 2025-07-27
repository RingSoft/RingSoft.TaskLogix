using System.Windows.Controls;
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
            
        }
    }
}
