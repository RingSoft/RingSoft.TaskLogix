using System.Windows.Controls;
using RingSoft.DataEntryControls.Engine;
using RingSoft.DataEntryControls.WPF;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.App.TaskMaintenance
{
    /// <summary>
    /// Interaction logic for TaskRecurWeeklyUserControl.xaml
    /// </summary>
    public partial class TaskRecurWeeklyUserControl
    {
        private VmUiControl _sunUiControl;
        private VmUiControl _munUiControl;
        private VmUiControl _tueUiControl;
        private VmUiControl _wedUiControl;
        private VmUiControl _thuUiControl;
        private VmUiControl _friUiControl;
        private VmUiControl _satUiControl;

        public TaskRecurWeeklyUserControl()
        {
            InitializeComponent();

            _sunUiControl = new VmUiControl(SunCheck, LocalViewModel.SunUiCommand);
            _munUiControl = new VmUiControl(MonCheck, LocalViewModel.MonUiCommand);
            _tueUiControl = new VmUiControl(TueCheck, LocalViewModel.TueUiCommand);
            _wedUiControl = new VmUiControl(WedCheck, LocalViewModel.WedUiCommand);
            _thuUiControl = new VmUiControl(ThuCheck, LocalViewModel.ThuUiCommand);
            _friUiControl = new VmUiControl(FriCheck, LocalViewModel.FriUiCommand);
            _satUiControl = new VmUiControl(SatCheck, LocalViewModel.SatUiCommand);

            Loaded += (sender, args) =>
            {
                LocalViewModel.SetEnabled();
            };
        }

        public override TaskRecurViewModelBase GetRecurViewModel()
        {
            return LocalViewModel;
        }

        public override void SetInitialFocus()
        {
            switch (LocalViewModel.RecurType)
            {
                case WeeklyRecurTypes.EveryXWeeks:
                    EveryXWeeksRadio.Focus();
                    break;
                case WeeklyRecurTypes.RegenerateXWeeksAfterCompleted:
                    RegenRadio.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
