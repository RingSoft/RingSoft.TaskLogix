using System.Windows;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurWindow.xaml
    /// </summary>
    public partial class TaskRecurWindow : Window
    {
        public TaskProcessor TaskProcessor { get; }

        public bool DialogResult { get; private set; }

        public TaskRecurWindow(TaskProcessor taskProcessor)
        {
            TaskProcessor = taskProcessor;
            InitializeComponent();
        }
    }
}
