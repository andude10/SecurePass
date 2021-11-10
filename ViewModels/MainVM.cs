using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace SecurePass.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public MainVM()
        {
            AccountsVM = new AccountsVM();
            NotesVM = new NotesVM();
        }
        private BaseViewModel _currentContent;
        public BaseViewModel CurrentContent
        {
            get { return _currentContent; }
            set { RaisePropertyChanged(ref _currentContent, value); }
        }

        public AccountsVM AccountsVM { get; }
        public HistoryVM HistoryVM { get; set; }
        public NotesVM NotesVM { get; set; }
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
                    //a new instance of HistroyVM is created each time to update the changes
                    HistoryVM = new HistoryVM();
                    CurrentContent = HistoryVM;
                });
            }
        }
        private ICommand _openNotesPage;
        public ICommand OpenNotesPage
        {
            get
            {
                return _openNotesPage ??= new RelayCommand(() =>
                {
                    CurrentContent = NotesVM;
                });
            }
        }
        #endregion
    }
}
