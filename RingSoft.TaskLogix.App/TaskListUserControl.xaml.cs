using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Controls.WPF;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App
{
    /// <summary>
    /// Interaction logic for TaskListUserControl.xaml
    /// </summary>
    public partial class TaskListUserControl : ITaskListView
    {
        public TaskListUserControl()
        {
            InitializeComponent();

            ListBox.MouseDoubleClick += (sender, args) =>
            {
                LocalViewModel.OpenTaskCommand.Execute(null);
            };

            ListBox.KeyDown += (sender, args) =>
            {
                if (args.Key == Key.Enter)
                {
                    LocalViewModel.OpenTaskCommand.Execute(null);
                    args.Handled = true;
                }
            };
        }

        public void ShowMaintenanceTab(PrimaryKeyValue primaryKey)
        {
            LookupControlsGlobals.TabControl.ShowAddView(primaryKey);
        }
    }
}
