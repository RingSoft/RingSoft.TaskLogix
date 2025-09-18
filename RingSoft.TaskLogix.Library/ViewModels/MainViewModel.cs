using System.Collections.ObjectModel;
using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;
using Timer = System.Timers.Timer;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface IMainView
    {
        void ShowReminders(List<Reminder> reminders);

        void ShowReminderTimer(List<Reminder> reminders);

        void CloseReminders();

        bool CloseAllTabs();

        void ShowTaskListPanel(bool show = true);

        void ShowBalloon(List<Reminder> reminders);

        void SetGreenAlert();
    }
    public class MainViewModel : TemplateMainViewModel
    {
        public IMainView MainView { get; set; }
        public RelayCommand ChangeDatabaseCommand { get; }
        public RelayCommand ManageTasksCommand { get; }
        public RelayCommand ShowAdvFindTabCommand { get; }
        public List<Reminder> BalloonsShown { get; }
        public List<TaskMaintenanceViewModel> TaskViewModels { get; } = new List<TaskMaintenanceViewModel>();

        private Timer _timer = new Timer(1000);
        private int _seconds = 0;

        public MainViewModel()
        {
            ChangeDatabaseCommand = new RelayCommand((() =>
            {
                if (MainView.CloseAllTabs())
                {
                    EnableTimer(false);
                    ChangeMaster();
                }
            }));
            ManageTasksCommand = new RelayCommand((() =>
            {
                View.ShowMaintenanceUserControl(AppGlobals.LookupContext.Tasks);
            }));
            ShowAdvFindTabCommand = new RelayCommand(ShowAdvFindTab);
            AppGlobals.MainViewModel = this;

            _timer.Elapsed += (sender, args) =>
            {
                _seconds++;
                if (_seconds >= 60)
                {
                    HandleRemindersTimer();
                }
            };

            BalloonsShown = new List<Reminder>();
        }

        public override void Initialize(ITemplateMainView view)
        {
            if (view is IMainView mainView)
            {
                MainView = mainView;
            }
            base.Initialize(view);
        }

        protected override bool PostInitialize()
        {
            MainView.ShowTaskListPanel();
            View.ShowMaintenanceUserControl(AppGlobals.LookupContext.Tasks);
            BalloonsShown.Clear();
            HandleRemindersTimer();
            EnableTimer();
            return true;
        }

        public void EnableTimer(bool enable = true)
        {
            _timer.Enabled = enable;
            if (enable)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
                _seconds = 0;
            }
        }

        public List<Reminder> GetReminders()
        {
            var result = new List<Reminder>();
            var context = SystemGlobals.DataRepository.GetDataContext();
            if (context != null)
            {
                var table = context.GetTable<TlTask>();
                if (table != null)
                {
                    var remindersInDb = table.Where(p => p.ReminderDateTime != null
                                                         && p.ReminderDateTime < GblMethods.NowDate())
                        .OrderBy(p => p.ReminderDateTime);

                    if (remindersInDb.Any())
                    {
                        foreach (var taskReminder in remindersInDb)
                        {
                            var addReminder = !(taskReminder.SnoozeDateTime != null
                                                && taskReminder.SnoozeDateTime > GblMethods.NowDate());

                            if (taskReminder.StatusType == (byte)TaskStatusTypes.Completed)
                            {
                                addReminder = false;
                            }

                            if (addReminder)
                            {
                                var reminder = new Reminder
                                {
                                    TaskId = taskReminder.Id,
                                    Subject = taskReminder.Subject,
                                    Date = taskReminder.StartDate.ToLongDateString(),
                                    ReminderDateTime = taskReminder.GetReminderDateTime(),
                                };
                                result.Add(reminder);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void HandleReminders()
        {
            var reminders = GetReminders();
            if (MainView != null)
            {
                if (reminders.Any())
                {
                    MainView.ShowReminders(reminders);
                }
                else
                {
                    MainView.CloseReminders();
                }
            }
        }

        public void HandleRemindersTimer()
        {
            EnableTimer(false);
            var reminders = GetReminders();
            if (MainView != null)
            {
                if (reminders.Any())
                {
                    var balloonsToShow = new List<Reminder>();

                    foreach (var reminder in reminders)
                    {
                        var addBalloon = !BalloonsShown.Any(
                            p => p.TaskId == reminder.TaskId
                                 && p.ReminderDateTime == reminder.ReminderDateTime);

                        if (addBalloon)
                        {
                            BalloonsShown.Add(reminder);
                            balloonsToShow.Add(reminder);
                        }
                    }
                    if (balloonsToShow.Any())
                    {
                        EnableTimer();
                        MainView.ShowBalloon(balloonsToShow);
                        MainView.ShowReminderTimer(reminders);
                    }
                }
            }
            EnableTimer();
        }

        private void ShowAdvFindTab()
        {
            View.ShowMaintenanceUserControl(AppGlobals.LookupContext.AdvancedFinds);
        }

        public void RefreshTaskViewModels(int taskId)
        {
            var viewModels = TaskViewModels.Where(p => p.Id == taskId);
            if (viewModels.Any())
            {
                foreach (var taskMaintenanceViewModel in viewModels)
                {
                    taskMaintenanceViewModel.RefreshFromDatabase();
                }
            }
        }

        public void RemoveFromBalloonsShown(int taskId)
        {
            var existBalloonsShown = BalloonsShown.Where(p => p.TaskId == taskId)
                .ToList();
            foreach (var reminder in existBalloonsShown)
            {
                BalloonsShown.Remove(reminder);
            }
        }
    }
}
