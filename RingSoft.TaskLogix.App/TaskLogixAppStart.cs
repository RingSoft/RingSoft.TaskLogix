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
        private Application _application;

        public TaskLogixAppStart(Application application)
            : base(application, new AppGlobals(), new MainWindow())
        {
            _application = application;
        }

        protected override void RegisterWindows()
        {
            var factory = new TaskLogixControlContentFactory(_application);

            LookupControlsGlobals.WindowRegistry.RegisterUserControl<TaskMaintenanceUserControl, TlTask>();
            LookupControlsGlobals.WindowRegistry.RegisterUserControl<TaskHistoryUserControl, TlTaskHistory>();
        }
    }
}
