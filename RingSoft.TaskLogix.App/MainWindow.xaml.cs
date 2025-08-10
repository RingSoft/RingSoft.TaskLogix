using System.Collections.ObjectModel;
using System.ComponentModel;
using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DbLookup.Controls.WPF;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainView
    {
        public override TemplateMainViewModel TemplateMainViewModel => ViewModel;
        public override ITemplateMainView View => this;

        private RemindersWindow _remindersWindow;

        public MainWindow()
        {
            InitializeComponent();
            LookupControlsGlobals.SetTabSwitcherWindow(this, TabControl);
            TabControl.SetDestionationAsFirstTab = false;
        }

        public void ShowReminders(List<Reminder> reminderList)
        {
            if (_remindersWindow == null)
            {
                _remindersWindow = new RemindersWindow(reminderList);
                _remindersWindow.Closed += (sender, args) =>
                {
                    _remindersWindow = null;
                };
                LookupControlsGlobals.WindowRegistry.ShowDialog(_remindersWindow);
            }
            else
            {
                _remindersWindow.LocalViewModel.ProcessNewReminders(reminderList);
            }
        }

        public void CloseReminders()
        {
            if (_remindersWindow != null)
            {
                _remindersWindow.Close();
                _remindersWindow = null;
            }
        }

        public bool CloseAllTabs()
        {
            return TabControl.CloseAllTabs();
        }

        public void ShowTaskListPanel(bool show = true)
        {
            TaskListPanel.Children.Clear();
            if (show)
            {
                var today = new TaskListUserControl();
                today.LocalViewModel.Initialize(TaskListTypes.Today);
                if (today.LocalViewModel.TaskList.Any())
                {
                    TaskListPanel.Children.Add(today);
                    TaskListPanel.UpdateLayout();
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !CloseAllTabs();
            base.OnClosing(e);
        }
    }
}