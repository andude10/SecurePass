using SecurePass.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SecurePass.Businesslogic;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class AccountsVM : BaseViewModel
    {
        public AccountsVM()
        {
            Accounts = AccountModelBL.GetAccounts().ToList();
            Categories = new ObservableCollection<string>();
            SelectedAccounts = new ObservableCollection<Account>(Accounts);
            UpdateView();
        }

        private List<Account> _accounts;
        public List<Account> Accounts
        {
            get { return _accounts; }
            set { RaisePropertyChanged(ref _accounts, value); }
        }
        private ObservableCollection<Account> _selectedAccounts;
        public ObservableCollection<Account> SelectedAccounts
        {
            get { return _selectedAccounts; }
            set { RaisePropertyChanged(ref _selectedAccounts, value); }
        }
        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set { RaisePropertyChanged(ref _categories, value); }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set 
            {
                RaisePropertyChanged(ref _selectedCategory, value);
                SelectedAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
            }
        }
        private Account _selectedOneAccount;
        public Account SelectedOneAccount
        {
            get { return _selectedOneAccount; }
            set { RaisePropertyChanged(ref _selectedOneAccount, value);}
        }

        #region Commands
        private ICommand _addAccount;
        public ICommand AddAccount
        {
            get
            {
                return _addAccount ??= new RelayCommand(() =>
                    {
                        Account newAccount;
                        try
                        {
                            newAccount = WeakReferenceMessenger.Default.Send<NewAccountWindowMessage>();
                        }
                        catch (System.InvalidOperationException)
                        { return; }

                        AccountModelBL.AddAccount(newAccount);
                        Accounts.Add(newAccount);
                        UpdateView();
                    });
            }
        }
        private ICommand _editAccount;
        public ICommand EditAccount
        {
            get
            {
                return _editAccount ??= new RelayCommand<int>(obj =>
                {
                    int id = obj;
                    Account account = Accounts.Find(a => a.AccountId == id);
                    try
                    {
                        EditAccountWindowMessage editWindow = new EditAccountWindowMessage(account);
                        account = WeakReferenceMessenger.Default.Send(editWindow);
                    }
                    catch (System.InvalidOperationException)
                    { return; }
                    AccountModelBL.SetAccount(account);
                    UpdateView();
                });
            }
        }
        private ICommand _deleteAccount;
        public ICommand DeleteAccount
        {
            get
            {
                return _deleteAccount ??= new RelayCommand<int>(obj =>
                {
                    int id = obj;
                    Account account = Accounts.Find(a => a.AccountId == id);
                    Accounts.Remove(account);
                    AccountModelBL.RemoveAccount(account);
                    UpdateView();
                });
            }
        }
        #endregion
        private void UpdateView()
        {
            var categories = Accounts.Select(a => a.Category).ToList();
            Categories = new ObservableCollection<string>(categories.Distinct().ToList());
            SelectedAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
        }
    }
}
