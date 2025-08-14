using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DbLookup;
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

        public new bool IsActive
        {
            get
            {
                Dispatcher.Invoke(() =>
                {
                    return base.IsActive;
                });
                return true;
            }
        }

        public bool ShowRemindersOnActivate { get; set; }

        private RemindersWindow _remindersWindow;

        public MainWindow()
        {
            InitializeComponent();
            LookupControlsGlobals.SetTabSwitcherWindow(this, TabControl);
            TabControl.SetDestionationAsFirstTab = false;
        }

        protected override void OnActivated(EventArgs e)
        {
            if (_remindersWindow != null)
            {
                _remindersWindow.Activate();
            }
            else
            {
                ActivateTab();
            }
            base.OnActivated(e);
        }

        private void ActivateTab()
        {
            if (TabControl.SelectedItem != null)
            {
                var tabItem = TabControl.SelectedItem as DbMaintenanceTabItem;
                if (tabItem != null)
                {
                    tabItem.Focus();
                    tabItem.UserControl.SetInitialFocus();
                }
            }
        }

        public void ShowReminders(List<Reminder> reminderList)
        {
            if (_remindersWindow == null)
            {
                Dispatcher.Invoke(() =>
                {
                    _remindersWindow = new RemindersWindow(reminderList);
                    _remindersWindow.Closed += (sender, args) =>
                    {
                        _remindersWindow = null;
                        AppGlobals.MainViewModel.MainView.ShowTaskListPanel();
                        Activate();
                        ActivateTab();
                    };

                    _remindersWindow.ShowInTaskbar = false;
                    _remindersWindow.Owner = this;
                    _remindersWindow.ShowDialog();
                });
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
                AppGlobals.MainViewModel.MainView.ShowTaskListPanel();
            }
        }

        public bool IsRemindersOpen => _remindersWindow != null;

        public bool CloseAllTabs()
        {
            var result = TabControl.CloseAllTabs();
            if (result)
            {
                ShowTaskListPanel(false);
            }

            return result;
        }

        public void ShowTaskListPanel(bool show = true)
        {
            TaskListPanel.Children.Clear();
            TaskListPanel.UpdateLayout();
            if (show)
            {
                for (int i = 0; i <= (int)TaskListTypes.Older; i++)
                {
                    var type = (TaskListTypes)i;
                    var listControl = new TaskListUserControl();
                    listControl.LocalViewModel.Initialize(type, listControl);
                    if (listControl.LocalViewModel.TaskList.Any())
                    {
                        TaskListPanel.Children.Add(listControl);
                        TaskListPanel.UpdateLayout();
                    }
                }
            }
        }

        public void ShowBalloon(List<Reminder> reminders)
        {
            foreach (var reminder in reminders)
            {
                ShowBalloon(reminder.Subject);
            }
        }

        private void ShowBalloon(string text)
        {
            Dispatcher.Invoke(() =>
            {
                LookupControlsGlobals.LookupWindowFactory.SetAlertLevel(AlertLevels.Yellow, false
                    , this, text);
            });

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !CloseAllTabs();
            base.OnClosing(e);
        }
    }
}