using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using SecurePass.Repositories.UnitOfWork;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class MainVm : BaseViewModel
    {
        public delegate void SearchingHandler(string enteredText);

        private readonly IUnitOfWork _unitOfWork;
        private BaseViewModel _currentContent;

        public MainVm(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ViewModelManager.AccountsVm = new AccountsVm(unitOfWork);
            ViewModelManager.NotesVm = new NotesVm(unitOfWork);
            OnSearching += ViewModelManager.AccountsVm.SearchingData;
            OnSearching += ViewModelManager.NotesVm.SearchingData;

            CurrentContent = ViewModelManager.AccountsVm;
        }

        public BaseViewModel CurrentContent
        {
            get => _currentContent;
            set => RaisePropertyChanged(ref _currentContent, value);
        }

        public event SearchingHandler OnSearching;

        #region Commands

        private ICommand _openPasswordsPage;

        public ICommand OpenPasswordsPage => _openPasswordsPage ??= new RelayCommand(() =>
        {
            CurrentContent = ViewModelManager.AccountsVm;
        });

        private ICommand _openHistoryPage;

        public ICommand OpenHistoryPage => _openHistoryPage ??= new RelayCommand(() =>
        {
            //a new instance of HistoryVm is created each time to update the changes
            ViewModelManager.HistoryVm = new HistoryVm(_unitOfWork);
            OnSearching += ViewModelManager.HistoryVm.SearchingData;
            CurrentContent = ViewModelManager.HistoryVm;
        });

        private ICommand _openNotesPage;

        public ICommand OpenNotesPage => _openNotesPage ??= new RelayCommand(() =>
        {
            CurrentContent = ViewModelManager.NotesVm;
        });

        private ICommand _searching;

        public ICommand Searching => _searching ??= new RelayCommand<string>(obj => { OnSearching?.Invoke(obj); });

        #endregion
    }
}