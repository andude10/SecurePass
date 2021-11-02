using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

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
        private ICommand _openPasswordsPage;
        public ICommand OpenPasswordsPage
        {
            get
            {
                return _openPasswordsPage ??= new RelayCommand(()=>
                {
                    CurrentContent = AccountsVM;
                });
            }
        }
        #endregion
    }
}
