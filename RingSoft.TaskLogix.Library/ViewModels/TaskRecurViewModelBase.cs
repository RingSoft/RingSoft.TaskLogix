using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public abstract class TaskRecurViewModelBase : INotifyPropertyChanged
    {
        public abstract void SetInitialValues();

        public abstract void LoadFromTaskProcessor(TaskProcessor taskProcessor);

        public abstract void SaveToTaskProcessor(TaskProcessor taskProcessor);

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
