using SecurePass.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public MainVM()
        {
            AccountsVM = new AccountsVM();
        }
        private BaseViewModel _currentContent;
        public BaseViewModel CurrentContent
        {
            get { return _currentContent; }
            set { RaisePropertyChanged(ref _currentContent, value); }
        }

        public AccountsVM AccountsVM { get; }

        #region Commands
        private Command _openPasswordsPage;
        public Command OpenPasswordsPage
        {
            get
            {
                return _openPasswordsPage ??= new Command(obj =>
                {
                    CurrentContent = AccountsVM;
                });
            }
        }
        #endregion
    }
}
