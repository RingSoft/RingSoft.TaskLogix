using System.Collections.ObjectModel;
using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface IMainView
    {
        void ShowReminders(List<Reminder> ReminderList);

        void CloseReminders();
    }
    public class MainViewModel : TemplateMainViewModel
    {
        public IMainView MainView { get; set; }
        public RelayCommand ManageTasksCommand { get; }
        public RelayCommand ShowAdvFindTabCommand { get; }

        public MainViewModel()
        {
            ManageTasksCommand = new RelayCommand((() =>
            {
                View.ShowMaintenanceUserControl(AppGlobals.LookupContext.Tasks);
            }));
            ShowAdvFindTabCommand = new RelayCommand(ShowAdvFindTab);
            AppGlobals.MainViewModel = this;
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
            HandleReminders();
            return true;
        }

        public void HandleReminders()
        {
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
                                MainView.ShowReminders(remindersList);
                            }
                            else
                            {
                                MainView.CloseReminders();
                            }
                        }
                    }
                    else
                    {
                        if (MainView != null) MainView.CloseReminders();
                    }
                }
            }
        }

        private void ShowAdvFindTab()
        {
            View.ShowMaintenanceUserControl(AppGlobals.LookupContext.AdvancedFinds);
        }
    }
}
