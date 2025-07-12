using System.Windows;
using RingSoft.CustomTemplate.Controls.WPF;
using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Controls.WPF;
using RingSoft.TaskLogix.App.TaskMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;

namespace RingSoft.TaskLogix.App
{
    public class TaskLogixAppStart : TemplateAppStart
    {
        public TaskLogixAppStart(Application application)
            : base(application, new AppGlobals(), new MainWindow())
        {
        }

        protected override void RegisterWindows()
        {
            LookupControlsGlobals.WindowRegistry.RegisterUserControl<TaskMaintenanceUserControl, TlTask>();
        }
    }
}
