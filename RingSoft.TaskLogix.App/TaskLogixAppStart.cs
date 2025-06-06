using System.Windows;
using RingSoft.CustomTemplate.Controls.WPF;
using RingSoft.CustomTemplate.Library;
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
            
        }
    }
}
