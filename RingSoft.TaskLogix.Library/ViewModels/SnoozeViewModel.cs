﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using RingSoft.DataEntryControls.Engine;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.ViewModels
{
    public interface ISnoozeView
    {
        void Close();
    }

    public class SnoozeViewModel : INotifyPropertyChanged
    {
        private DateTime _snoozeDateTime;

        public DateTime SnoozeDateTime
        {
            get { return _snoozeDateTime; }
            set
            {
                if (_snoozeDateTime == value)
                {
                    return;
                }
                _snoozeDateTime = value;
                OnPropertyChanged();
            }
        }

        public ISnoozeView View { get; private set; }

        public bool DialogResult { get; private set; }

        public RelayCommand OkCommand { get; }

        public RelayCommand CancelCommand { get; }

        private TlTask _task;

        public SnoozeViewModel()
        {
            OkCommand = new RelayCommand(OnOK);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public void Init(ISnoozeView view, TlTask task)
        {
            View = view;
            _task = task;

            var snoozeDate = _task.SnoozeDateTime;
            if (snoozeDate == null)
            {
                snoozeDate = DateTime.Today;
            }

            snoozeDate = snoozeDate.Value.AddDays(1);
            snoozeDate = new DateTime(snoozeDate.Value.Year, snoozeDate.Value.Month, snoozeDate.Value.Day, 7, 0, 0);

            SnoozeDateTime = snoozeDate.Value;
        }

        private void OnOK()
        {
            _task.SnoozeDateTime = SnoozeDateTime;
            DialogResult = true;
            View.Close();
        }

        private void OnCancel()
        {
            View.Close();
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
