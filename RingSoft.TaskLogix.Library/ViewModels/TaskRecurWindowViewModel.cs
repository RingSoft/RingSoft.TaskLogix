using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface IRecurWindowView
    {
        TaskProcessor TaskProcessor { get; set; }
        void UpdateRecurType();
    }
    public class TaskRecurWindowViewModel : INotifyPropertyChanged
    {
        private TaskRecurTypes _recurType;

        public TaskRecurTypes RecurType
        {
            get { return _recurType; }
            set
            {
                if (_recurType == value)
                {
                    return;
                }

                _recurType = value;
                OnPropertyChanged();
                View.UpdateRecurType();
            }
        }

        public IRecurWindowView View { get; private set; }

        public void Init(IRecurWindowView view)
        {
            View = view;

            var recurType = view.TaskProcessor.RecurType;
            if (recurType == TaskRecurTypes.None)
            {
                recurType = TaskRecurTypes.Weekly;
            }
            RecurType = recurType;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
