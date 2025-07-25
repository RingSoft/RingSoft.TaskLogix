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
                    WeeklyRadio.Focus();
                    break;
                case TaskRecurTypes.Monthly:
                    MonthlyRadio.Focus();
                    break;
                case TaskRecurTypes.Yearly:
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

        public bool DialogResult { get; private set; }

        public TaskRecurWindow(TaskProcessor taskProcessor)
        {
            TaskProcessor = taskProcessor;
            InitializeComponent();
            LocalViewModel.Init(this);
        }
    }
}
