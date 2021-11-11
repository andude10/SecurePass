using SecurePass.Models;
using System.Collections.ObjectModel;
using SecurePass.Businesslogic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;
using System.Collections.Generic;

namespace SecurePass.ViewModels
{
    public class HistoryVM : BaseViewModel
    {
        public HistoryVM()
        {
            AccountChanges = AccountModelBL.GetAccountsChanges().ToList();
            // reverse so that new changes are at the top
            AccountChanges.Reverse();

            ActualAccountChanges = new ObservableCollection<AccountChange>(AccountChanges);
        }
        private List<AccountChange> _accountChanges;
        public List<AccountChange> AccountChanges
        {
            get { return _accountChanges; }
            set { RaisePropertyChanged(ref _accountChanges, value); }
        }
        private ObservableCollection<AccountChange> _actualAccountChanges;
        public ObservableCollection<AccountChange> ActualAccountChanges
        {
            get { return _actualAccountChanges; }
            set { RaisePropertyChanged(ref _actualAccountChanges, value); }
        }

        #region Command
        private ICommand _copyChangetext;
        public ICommand CopyChangetext
        {
            get
            {
                return _copyChangetext ??= new RelayCommand<int>(obj =>
                {
                    int id = obj;
                    Clipboard.SetText(AccountChanges.Find(ch => ch.AccountChangeId == id).Change);
                });
            }
        }
        #endregion
        public override void SearchingData(string enteredText)
        {
            ActualAccountChanges = new ObservableCollection<AccountChange>(AccountChanges.Where(ach =>
            {
                var alltext = ach.Change + ach.Date;
                return alltext.ToLower().Contains(enteredText.ToLower());
            }));
        }
    }
}
