using RingSoft.App.Controls;
using RingSoft.DataEntryControls.WPF;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Controls.WPF;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.Library;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    public class TaskHeaderControl : DbMaintenanceCustomPanel
    {
        public DbMaintenanceButton MarkCompleteButton { get; set; }

        public DbMaintenanceButton RecurrenceButton { get; set; }

        static TaskHeaderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TaskHeaderControl)
                , new FrameworkPropertyMetadata(typeof(TaskHeaderControl)));
        }

        public override void OnApplyTemplate()
        {
            MarkCompleteButton = GetTemplateChild(nameof(MarkCompleteButton)) as DbMaintenanceButton;
            RecurrenceButton = GetTemplateChild(nameof(RecurrenceButton)) as DbMaintenanceButton;

            base.OnApplyTemplate();
        }
    }
    /// <summary>
    /// Interaction logic for TaskMaintenanceUserControl.xaml
    /// </summary>
    public partial class TaskMaintenanceUserControl : ITaskMaintenanceView
    {
        public TaskMaintenanceUserControl()
        {
            InitializeComponent();
            RegisterFormKeyControl(SubjectControl);

            TopHeaderControl.Loaded += (sender, args) =>
            {
                if (TopHeaderControl.CustomPanel is TaskHeaderControl taskHeaderControl)
                {
                    taskHeaderControl.MarkCompleteButton.Command =
                        LocalViewModel.MarkCompleteCommand;
                    taskHeaderControl.RecurrenceButton.Command =
                        LocalViewModel.RecurrenceCommand;

                    taskHeaderControl.MarkCompleteButton.ToolTip.HeaderText = "Mark Complete (Ctrl + T, Ctrl + M)";
                    taskHeaderControl.MarkCompleteButton.ToolTip.DescriptionText = "Mark this task as complete.";

                    taskHeaderControl.RecurrenceButton.ToolTip.HeaderText = "Setup Recurrence (Ctrl + T, Ctrl + R)";
                    taskHeaderControl.RecurrenceButton.ToolTip.DescriptionText =
                        "Setup recurrence for this task.";
                }
            };

            var hotKey = new HotKey(LocalViewModel.MarkCompleteCommand);
            hotKey.AddKey(Key.T);
            hotKey.AddKey(Key.M);
            AddHotKey(hotKey);

            hotKey = new HotKey(LocalViewModel.RecurrenceCommand);
            hotKey.AddKey(Key.T);
            hotKey.AddKey(Key.R);
            AddHotKey(hotKey);

            LocalViewModel.Init(this);
        }

        protected override DbMaintenanceViewModelBase OnGetViewModel()
        {
            return LocalViewModel;
        }

        protected override Control OnGetMaintenanceButtons()
        {
            return TopHeaderControl;
        }

        protected override DbMaintenanceStatusBar OnGetStatusBar()
        {
            return StatusBar;
        }

        protected override string GetTitle()
        {
            return "Tasks";
        }

        public bool ShowTaskRecurrenceWindow()
        {
            var win = new TaskRecurWindow(LocalViewModel.TaskProcessor);
            LookupControlsGlobals.WindowRegistry.ShowDialog(win);
            return win.DialogResult;
        }
    }
}
