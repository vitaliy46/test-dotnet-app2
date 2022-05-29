using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace aiPeopleTracker.Core.Common
{
    /// <summary>
    /// Объект, уведомляющий об изменении своих свойств
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            var result = false;

            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                result = true;

                field = value;

                OnPropertyChanged(propertyName);
            }

            return result;
        }
    }

   
}