using System.Windows;
using System.Windows.Controls;
using RingSoft.TaskLogix.Library.ViewModels;
using System.Windows.Input;
using RingSoft.DataEntryControls.WPF;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    public class RecurControlRadioButton : TlControlRadioButton
    {
        public TaskRecurWindow RecurWindow { get; private set; }

        public RecurControlRadioButton()
        {
            Loaded += (sender, args) =>
            {
                RecurWindow = this.GetParentOfType<TaskRecurWindow>();
            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    RecurWindow.SetFocusToRecurRadio();
                    e.Handled = true;
                    return;
                }
            }

            base.OnKeyDown(e);
        }
    }
    public abstract class TaskRecurUserControlBase : UserControl
    {
        public abstract TaskRecurViewModelBase GetRecurViewModel();

        public abstract void SetInitialFocus();
    }
}
