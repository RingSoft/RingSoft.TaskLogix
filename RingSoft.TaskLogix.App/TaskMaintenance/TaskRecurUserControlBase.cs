using System.Windows;
using System.Windows.Controls;
using RingSoft.TaskLogix.Library.ViewModels;
using System.Windows.Input;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    public abstract class TaskRecurUserControlBase : UserControl
    {
        public abstract TaskRecurViewModelBase GetRecurViewModel();

        public abstract void SetInitialFocus();
    }
}
