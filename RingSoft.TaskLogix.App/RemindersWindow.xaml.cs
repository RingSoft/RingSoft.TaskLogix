using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App
{
    /// <summary>
    /// Interaction logic for RemindersWindow.xaml
    /// </summary>
    public partial class RemindersWindow : IReminderView
    {
        public RemindersWindow(List<Reminder> reminders)
        {
            InitializeComponent();
            LocalViewModel.Initialize(this, reminders);
        }

        public void CloseWindow()
        {
            Close();
        }

        public void ResetSelection()
        {
            ListBox.SelectedItem = ListBox.Items[0];
            ListBox.Focus();
        }

        public DateTime? GetSnoozeDateTime(TlTask tlTask)
        {
            throw new NotImplementedException();
        }
    }
}
