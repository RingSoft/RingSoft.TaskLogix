using System.Windows.Controls;
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
            
        }
    }
}
