using System.Collections.ObjectModel;
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
                _remindersWindow.ShowDialog();
            }
            else
            {
                _remindersWindow.LocalViewModel.ProcessNewReminders(reminderList);
            }
        }
    }
}