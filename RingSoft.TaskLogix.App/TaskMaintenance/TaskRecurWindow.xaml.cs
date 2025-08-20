using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RingSoft.DataEntryControls.WPF;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    public class RecurRadioButton : RadioButton
    {
        public TaskRecurWindow RecurWindow { get; private set; }

        public static readonly DependencyProperty NextDownRadioButtonProperty =
            DependencyProperty.Register(nameof(NextDownRadioButton), typeof(RecurRadioButton), typeof(RecurRadioButton),
                new FrameworkPropertyMetadata(NextDownRadioButtonChangedCallback));

        /// <summary>
        /// Gets or sets the UI label.  This is a bind-able property.
        /// </summary>
        /// <value>The UI label.</value>
        public RecurRadioButton NextDownRadioButton
        {
            get { return (RecurRadioButton)GetValue(NextDownRadioButtonProperty); }
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
            var recurRadioButton = (RecurRadioButton)obj;
            if (recurRadioButton != null)
            {

            }
        }


        public RecurRadioButton()
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
                if (RecurWindow.ActiveRecurUserControl != null)
                {
                    RecurWindow.ActiveRecurUserControl.SetInitialFocus();
                    e.Handled = true;
                    return;
                }
            }

            if (e.Key == Key.Down)
            {
                if (NextDownRadioButton != null)
                {
                    NextDownRadioButton.Focus();
                    e.Handled |= true;
                }
            }

            base.OnKeyDown(e);
        }
    }
    /// <summary>
    /// Interaction logic for TaskRecurWindow.xaml
    /// </summary>
    public partial class TaskRecurWindow : IRecurWindowView
    {
        public TaskProcessor TaskProcessor { get; set; }

        public TaskRecurUserControlBase ActiveRecurUserControl { get; private set; }
        public void UpdateRecurType()
        {
            ActiveRecurUserControl = null;
            switch (LocalViewModel.RecurType)
            {
                case TaskRecurTypes.None:
                    break;
                case TaskRecurTypes.Daily:
                    ActiveRecurUserControl = new TaskRecurDailyUserControl();
                    DailyRadio.Focus();
                    break;
                case TaskRecurTypes.Weekly:
                    ActiveRecurUserControl = new TaskRecurWeeklyUserControl();
                    WeeklyRadio.Focus();
                    break;
                case TaskRecurTypes.Monthly:
                    ActiveRecurUserControl = new TaskRecurMonthlyUserControl();
                    MonthlyRadio.Focus();
                    break;
                case TaskRecurTypes.Yearly:
                    ActiveRecurUserControl = new TaskRecurYearlyUserControl();
                    YearlyRadio.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            RecurPanel.Children.Clear();
            if (ActiveRecurUserControl != null)
            {
                RecurPanel.Children.Add(ActiveRecurUserControl);
                LocalViewModel.ActiveRecurViewModel = ActiveRecurUserControl.GetRecurViewModel();
            }
            RecurPanel.UpdateLayout();
        }

        public void CloseWindow(bool result)
        {
            DialogResult = result;
            Close();
        }

        public bool DialogResult { get; private set; }

        public TaskRecurWindow(TaskProcessor taskProcessor)
        {
            TaskProcessor = taskProcessor;
            InitializeComponent();
            LocalViewModel.Init(this);
        }
    }
}
