using Microsoft.Toolkit.Mvvm.Input;
using SecurePass.Services;
using System.Windows.Input;

namespace SecurePass.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public MainVM()
        {
            ViewModelManager.AccountsVM = new AccountsVM();
            ViewModelManager.NotesVM = new NotesVM();
            OnSerching += ViewModelManager.AccountsVM.SearchingData;
            OnSerching += ViewModelManager.NotesVM.SearchingData;

            CurrentContent = ViewModelManager.AccountsVM;
        }
        public delegate void Serching(string enteredText);
        public event Serching OnSerching;

        private BaseViewModel _currentContent;
        public BaseViewModel CurrentContent
        {
            get { return _currentContent; }
            set 
            {
                RaisePropertyChanged(ref _currentContent, value); 
            }
        }
        #region Commands
        private ICommand _openPasswordsPage;
        public ICommand OpenPasswordsPage
        {
            get
            {
                return _openPasswordsPage ??= new RelayCommand(()=>
                {
                    CurrentContent = ViewModelManager.AccountsVM;
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
                    ViewModelManager.HistoryVM = new HistoryVM();
                    OnSerching += ViewModelManager.HistoryVM.SearchingData;
                    CurrentContent = ViewModelManager.HistoryVM;
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
                    CurrentContent = ViewModelManager.NotesVM;
                });
            }
        }
        private ICommand _searching;
        public ICommand Searching
        {
            get
            {
                return _searching ??= new RelayCommand<string>(obj =>
                {
                    OnSerching(obj);
                });
            }
        }
        #endregion
    }
}
