using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Businesslogic;
using SecurePass.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurePass.ViewModels
{
    public class LoginVM : BaseViewModel
    {
        public LoginVM()
        {
            DataBaseNames = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.db")
                                     .Select(Path.GetFileNameWithoutExtension)
                                     .ToList();
        }

        private List<string> _dataBaseNames;
        public List<string> DataBaseNames
        {
            get { return _dataBaseNames; }
            set { RaisePropertyChanged(ref _dataBaseNames, value); }
        }
        private SecureString _master_password;
        public SecureString Master_password
        {
            get { return _master_password; }
            set { RaisePropertyChanged(ref _master_password, value); }
        }
        private string _dbName;
        public string DbName
        {
            get { return _dbName; }
            set { RaisePropertyChanged(ref _dbName, value); }
        }

        private ICommand _login;
        public ICommand Login
        {
            get
            {
                return _login ??= new RelayCommand(() =>
                {
                    LoginWindowMessage message = new LoginWindowMessage(AccountModelBL.IsPasswordCorrect(Master_password, DbName));
                    WeakReferenceMessenger.Default.Send(message);
                });
            }
        }
    }
}
