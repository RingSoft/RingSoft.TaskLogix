using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurWindow.xaml
    /// </summary>
    public partial class TaskRecurWindow : IRecurWindowView
    {
        public TaskProcessor TaskProcessor { get; set; }
        public void UpdateRecurType()
        {
            switch (LocalViewModel.RecurType)
            {
                case TaskRecurTypes.None:
                    break;
                case TaskRecurTypes.Daily:
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
