using System.Windows;
using System.Windows.Input;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App
{
    /// <summary>
    /// Interaction logic for SnoozeWindow.xaml
    /// </summary>
    public partial class SnoozeWindow : ISnoozeView
    {
        public SnoozeWindow(TlTask task)
        {
            InitializeComponent();
            LocalViewModel.Init(this, task);

            Loaded += (sender, args) =>
            {
                LocalViewModel.UpdateUi();
            };

            SnoozeTimeBlockRadio.KeyDown += (sender, args) =>
            {
                if (args.Key == Key.Tab)
                {
                    if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
                    {
                        TimeBlockValueEditControl.Focus();
                        args.Handled = true;
                    }
                }
            };
        }
    }
}
