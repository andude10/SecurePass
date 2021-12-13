using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public delegate void SearchingHandler(string enteredText);

        private BaseViewModel _currentContent;

        public MainVM()
        {
            ViewModelManager.AccountsVM = new AccountsVM();
            ViewModelManager.NotesVM = new NotesVM();
            OnSearching += ViewModelManager.AccountsVM.SearchingData;
            OnSearching += ViewModelManager.NotesVM.SearchingData;

            CurrentContent = ViewModelManager.AccountsVM;
        }

        public BaseViewModel CurrentContent
        {
            get => _currentContent;
            set => RaisePropertyChanged(ref _currentContent, value);
        }

        public event SearchingHandler OnSearching;

        #region Commands

        private ICommand _openPasswordsPage;

        public ICommand OpenPasswordsPage
        {
            get
            {
                return _openPasswordsPage ??= new RelayCommand(() => { CurrentContent = ViewModelManager.AccountsVM; });
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
                    ViewModelManager.HistoryVM = new HistoryVM();
                    OnSearching += ViewModelManager.HistoryVM.SearchingData;
                    CurrentContent = ViewModelManager.HistoryVM;
                });
            }
        }

        private ICommand _openNotesPage;

        public ICommand OpenNotesPage
        {
            get { return _openNotesPage ??= new RelayCommand(() => { CurrentContent = ViewModelManager.NotesVM; }); }
        }

        private ICommand _searching;

        public ICommand Searching
        {
            get { return _searching ??= new RelayCommand<string>(obj => { OnSearching(obj); }); }
        }

        #endregion
    }
}