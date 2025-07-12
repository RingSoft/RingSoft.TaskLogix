using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DataEntryControls.Engine;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class MainViewModel : TemplateMainViewModel
    {
        public RelayCommand ManageTasksCommand { get; }
        public RelayCommand ShowAdvFindTabCommand { get; }

        public MainViewModel()
        {
            ManageTasksCommand = new RelayCommand((() =>
            {
                View.ShowMaintenanceUserControl(AppGlobals.LookupContext.Tasks);
            }));
            ShowAdvFindTabCommand = new RelayCommand(ShowAdvFindTab);
        }

        protected override bool PostInitialize()
        {
            return true;
        }

        private void ShowAdvFindTab()
        {
            View.ShowMaintenanceUserControl(AppGlobals.LookupContext.AdvancedFinds);
        }
    }
}
