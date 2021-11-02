using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SecurePass.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged<T>(ref T property, T newValue, Action callbackMethod, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callbackMethod?.Invoke();
        }
        public void RaisePropertyChanged<T>(ref T property, T newValue, Action<T> callbackMethod, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callbackMethod?.Invoke(newValue);
        }
    }
}
