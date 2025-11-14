using System.Collections.Generic;
using RingSoft.CustomTemplate.Library.ViewModels;
using RingSoft.DbLookup.ModelDefinition;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.Tests
{
    public class TestMainView : IMainView, ITemplateMainView
    {
        public void ShowReminders(List<Reminder> reminders)
        {
            
        }

        public void ShowReminderTimer(List<Reminder> reminders)
        {
            
        }

        public void CloseReminders()
        {
            
        }

        public bool CloseAllTabs()
        {
            return true;
        }

        public void ShowTaskListPanel(bool show = true)
        {
            
        }

        public void ShowBalloon(List<Reminder> reminders)
        {
            
        }

        public void SetGreenAlert()
        {
            
        }

        public bool ChangeMasterRecord()
        {
            return true;
        }

        public void ShowMaintenanceWindow(TableDefinitionBase tableDefinition)
        {
            
        }

        public void ShowMaintenanceUserControl(TableDefinitionBase tableDefinition, bool setAsFirstTab = true)
        {
            
        }

        public void ShowAdvancedFindWindow()
        {
            
        }

        public void Close()
        {
            
        }
    }

    public class TestTaskMaintView : ITaskMaintenanceView
    {
        public void ResetViewForNewRecord()
        {
            
        }

        public void SetReadOnlyMode(bool readOnlyValue)
        {
            
        }

        public bool ShowTaskRecurrenceWindow()
        {
            return true;
        }
    }
}
