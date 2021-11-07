using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Businesslogic;
using SecurePass.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurePass.ViewModels
{
    public class LoginVM : BaseViewModel
    {
        private List<string> _databases;
        public List<string> Databases
        {
            get { return _databases; }
            set { RaisePropertyChanged(ref _databases, value); }
        }
        private string _master_password;
        public string Master_password
        {
            get { return _master_password; }
            set { RaisePropertyChanged(ref _master_password, value); }
        }

        private ICommand _login;
        public ICommand Login
        {
            get
            {
                return _login ??= new RelayCommand(() =>
                {
                    LoginWindowMessage message = new LoginWindowMessage(AccountModelBL.IsPasswordCorrect(Master_password));
                    WeakReferenceMessenger.Default.Send(message);
                });
            }
        }
    }
}
