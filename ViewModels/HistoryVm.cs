using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using SecurePass.Models;
using SecurePass.Repositories.UnitOfWork;

namespace SecurePass.ViewModels
{
    public class HistoryVm : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<AccountChange> _accountChanges;
        private ObservableCollection<AccountChange> _actualAccountChanges;

        public HistoryVm(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            AccountChanges = _unitOfWork.AccountsChangesRepository.GetAccountsChanges().ToList();

            // reverse so that new changes are at the top
            AccountChanges.Reverse();

            ActualAccountChanges = new ObservableCollection<AccountChange>(AccountChanges);
        }

        public List<AccountChange> AccountChanges
        {
            get => _accountChanges;
            set => RaisePropertyChanged(ref _accountChanges, value);
        }

        public ObservableCollection<AccountChange> ActualAccountChanges
        {
            get => _actualAccountChanges;
            set => RaisePropertyChanged(ref _actualAccountChanges, value);
        }

        public override void SearchingData(string enteredText)
        {
            ActualAccountChanges = new ObservableCollection<AccountChange>(AccountChanges.Where(ach =>
            {
                var allText = ach.Change + ach.Date;
                return allText.ToLower().Contains(enteredText.ToLower());
            }));
        }

        #region Command

        private ICommand _copyChangeText;

        public ICommand CopyChangeText => _copyChangeText ??= new RelayCommand<int>(obj =>
        {
            var id = obj;
            var change = AccountChanges.Find(ch => ch.AccountChangeId == id)?.Change;

            if (change == null) return;

            Clipboard.SetText(change);
        });

        #endregion
    }
}