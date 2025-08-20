using System.Windows;
using System.Windows.Controls;
using RingSoft.DataEntryControls.WPF;
using RingSoft.TaskLogix.Library.ViewModels;
using System.Windows.Input;
using RingSoft.TaskLogix.SqlServer.Migrations;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    public class RecurControlRadioButton : RadioButton
    {
        public TaskRecurWindow RecurWindow { get; private set; }

        public static readonly DependencyProperty NextDownRadioButtonProperty =
            DependencyProperty.Register(nameof(NextDownRadioButton), typeof(RecurControlRadioButton), typeof(RecurControlRadioButton),
                new FrameworkPropertyMetadata(NextDownRadioButtonChangedCallback));

        /// <summary>
        /// Gets or sets the UI label.  This is a bind-able property.
        /// </summary>
        /// <value>The UI label.</value>
        public RecurControlRadioButton NextDownRadioButton
        {
            get { return (RecurControlRadioButton)GetValue(NextDownRadioButtonProperty); }
            set { SetValue(NextDownRadioButtonProperty, value); }
        }

        /// <summary>
        /// UIs the label changed callback.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void NextDownRadioButtonChangedCallback(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            var recurRadioButton = (RecurControlRadioButton)obj;
            if (recurRadioButton != null)
            {
                
            }
        }

        public RecurControlRadioButton NextUpRadioButton { get; private set; }

        public RecurControlRadioButton()
        {
            Loaded += (sender, args) =>
            {
                RecurWindow = this.GetParentOfType<TaskRecurWindow>();

                if (NextDownRadioButton != null)
                {
                    NextDownRadioButton.NextUpRadioButton = this;
                }
            };
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
            }

            if (e.Key == Key.Down)
            {
                if (NextDownRadioButton != null)
                {
                    NextDownRadioButton.Focus();
                    e.Handled |= true;
                }
            }

            if (e.Key == Key.Up)
            {
                if (NextUpRadioButton != null)
                {
                    NextUpRadioButton.Focus();
                    e.Handled |= true;
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
