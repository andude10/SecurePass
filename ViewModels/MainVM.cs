using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace SecurePass.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public MainVM()
        {
            AccountsVM = new AccountsVM();
            HistoryVM = new HistoryVM();
        }
        private BaseViewModel _currentContent;
        public BaseViewModel CurrentContent
        {
            get { return _currentContent; }
            set { RaisePropertyChanged(ref _currentContent, value); }
        }

        public AccountsVM AccountsVM { get; }
        public HistoryVM HistoryVM { get; set; }
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
        private ICommand _openHistoryPage;
        public ICommand OpenHistoryPage
        {
            get
            {
                return _openHistoryPage ??= new RelayCommand(() =>
                {
                    HistoryVM = new HistoryVM();
                    CurrentContent = HistoryVM;
                });
            }
        }
        #endregion
    }
}
