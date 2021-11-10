﻿using SecurePass.Models;
using System.Collections.ObjectModel;
using SecurePass.Businesslogic;
using System.Linq;

namespace SecurePass.ViewModels
{
    public class HistoryVM : BaseViewModel
    {
        public HistoryVM()
        {
            AccountChanges = new ObservableCollection<AccountChange>(AccountModelBL.GetAccountsChanges());
            // reverse so that new changes are at the top
            AccountChanges.Reverse();
        }
        private ObservableCollection<AccountChange> _accountChanges;
        public ObservableCollection<AccountChange> AccountChanges
        {
            get { return _accountChanges; }
            set { RaisePropertyChanged(ref _accountChanges, value); }
        }
        private AccountChange _selectedAccountChange;
        public AccountChange SelectedAccountChange
        {
            get { return _selectedAccountChange; }
            set { RaisePropertyChanged(ref _selectedAccountChange, value); }
        }
    }
}
