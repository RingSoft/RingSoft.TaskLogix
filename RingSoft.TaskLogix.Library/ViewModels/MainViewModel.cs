using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.DbLookup.ModelDefinition;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public class MainViewModel : TemplateMainViewModel
    {
        public MainViewModel()
        {
        }

        protected override bool PostInitialize()
        {
            return true;
        }
    }
}
