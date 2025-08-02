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

            ListBox.GotKeyboardFocus += (sender, args) => ListBox.SelectedItem ??= ListBox.Items[0];
        }

        public void CloseWindow()
        {
            Close();
        }
    }
}
