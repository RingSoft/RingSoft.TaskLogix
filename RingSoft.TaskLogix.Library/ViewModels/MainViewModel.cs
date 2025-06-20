using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.DbLookup.ModelDefinition;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class MainViewModel : TemplateMainViewModel
    {
        public RelayCommand ShowAdvFindTabCommand { get; }

        public MainViewModel()
        {
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
