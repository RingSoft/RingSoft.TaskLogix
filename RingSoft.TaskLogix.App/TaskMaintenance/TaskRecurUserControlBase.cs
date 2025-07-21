using System.Windows.Controls;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    public abstract class TaskRecurUserControlBase : UserControl
    {
        public abstract TaskRecurViewModelBase GetRecurViewModel();
    }
}
