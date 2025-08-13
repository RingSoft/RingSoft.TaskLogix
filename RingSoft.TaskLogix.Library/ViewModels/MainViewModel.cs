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
        void ShowReminders(List<Reminder> ReminderList);

        void CloseReminders();

        bool IsActive { get; }

        bool ShowRemindersOnActivate { get; set; }

        bool CloseAllTabs();

        void ShowTaskListPanel(bool show = true);

        void ShowBalloon(List<Reminder> reminders);
    }
    public class MainViewModel : TemplateMainViewModel
    {
        public IMainView MainView { get; set; }
        public RelayCommand ChangeDatabaseCommand { get; }
        public RelayCommand ManageTasksCommand { get; }
        public RelayCommand ShowAdvFindTabCommand { get; }

        public bool IsTimerActive => _timer.Enabled;

        public bool FinishedInit { get; private set; }

        private Timer _timer = new Timer(1000);

        public MainViewModel()
        {
            ChangeDatabaseCommand = new RelayCommand((() =>
            {
                if (MainView.CloseAllTabs())
                {
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
                _timer.Enabled = false;
                _timer.Stop();
                HandleReminders(true);
                _timer.Enabled = true;
                _timer.Start();
            };
        }

        public void ActivateTimer(bool activate = true)
        {
            _timer.Enabled = activate;
            if (activate)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
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
            HandleReminders();
            FinishedInit = true;
            _timer.Enabled = true;
            _timer.Start();
            return true;
        }

        public bool HandleReminders(bool fromTimer = false)
        {
            var result = false;
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
                        var remindersList = new List<Reminder>();
                        foreach (var taskReminder in remindersInDb)
                        {
                            var addReminder = true;
                            if (taskReminder.SnoozeDateTime != null
                                && taskReminder.SnoozeDateTime > GblMethods.NowDate())
                            {
                                addReminder = false;
                            }

                            if (taskReminder.RecurType == (byte)TaskRecurTypes.None)
                            {
                                if (taskReminder.StatusType == (byte)TaskStatusTypes.Completed)
                                {
                                    addReminder = false;
                                }
                            }

                            if (addReminder)
                            {
                                var reminder = new Reminder
                                {
                                    TaskId = taskReminder.Id,
                                    Subject = taskReminder.Subject,
                                    Date = taskReminder.StartDate.ToLongDateString(),
                                };
                                remindersList.Add(reminder);
                            }
                        }

                        if (MainView != null)
                        {
                            if (remindersList.Any())
                            {
                                if (fromTimer && !MainView.IsActive)
                                {
                                    MainView.ShowBalloon(remindersList);
                                }

                                if (!MainView.IsActive)
                                {
                                    MainView.ShowRemindersOnActivate = true;
                                }

                                if (MainView.ShowRemindersOnActivate && MainView.IsActive)
                                {
                                    MainView.ShowReminders(remindersList);
                                }
                                else
                                {
                                    if (!fromTimer)
                                    {
                                        MainView.ShowReminders(remindersList);
                                    }
                                }
                                result = true;
                            }
                            else
                            {
                                MainView.CloseReminders();
                            }
                        }
                    }
                    else
                    {
                        if (MainView != null) 
                            MainView.CloseReminders();
                    }
                }
            }

            return result;
        }

        private void ShowAdvFindTab()
        {
            View.ShowMaintenanceUserControl(AppGlobals.LookupContext.AdvancedFinds);
        }
    }
}
