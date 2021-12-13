using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Businesslogic;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class LoginVM : BaseViewModel
    {
        private List<string> _dataBaseNames;
        private string _dbName;

        private ICommand _login;
        private SecureString _masterPassword;

        public LoginVM()
        {
            DataBaseNames = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.db")
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
        }

        public List<string> DataBaseNames
        {
            get => _dataBaseNames;
            set => RaisePropertyChanged(ref _dataBaseNames, value);
        }

        public SecureString MasterPassword
        {
            get => _masterPassword;
            set => RaisePropertyChanged(ref _masterPassword, value);
        }

        public string DbName
        {
            get => _dbName;
            set => RaisePropertyChanged(ref _dbName, value);
        }

        public ICommand Login
        {
            get
            {
                return _login ??= new RelayCommand(() =>
                {
                    var message = new LoginWindowMessage(AccountModelBL.IsPasswordCorrect(MasterPassword, DbName));
                    WeakReferenceMessenger.Default.Send(message);
                });
            }
        }
    }
}