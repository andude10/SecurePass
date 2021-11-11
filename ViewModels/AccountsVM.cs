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
            ActualAccounts = new ObservableCollection<Account>(Accounts);
            UpdateView();
        }

        private List<Account> _accounts;
        public List<Account> Accounts
        {
            get { return _accounts; }
            set { RaisePropertyChanged(ref _accounts, value); }
        }
        private ObservableCollection<Account> _actualAccounts;
        public ObservableCollection<Account> ActualAccounts
        {
            get { return _actualAccounts; }
            set { RaisePropertyChanged(ref _actualAccounts, value); }
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
                ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
            }
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
                        if (newAccount.Category == null)
                            newAccount.Category = "No category";
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
                    string oldpass = account.Password;
                    try
                    {
                        EditAccountWindowMessage editWindow = new EditAccountWindowMessage(account);
                        account = WeakReferenceMessenger.Default.Send(editWindow);
                    }
                    catch (System.InvalidOperationException)
                    { return; }
                    if (account.Category == null)
                        account.Category = "No category";
                    AccountModelBL.SetAccount(account, oldpass);
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
        public override void SearchingData(string enteredText)
        {
            ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a =>
            {
                var alltext = a.Url + a.Username ;
                return alltext.ToLower().Contains(enteredText.ToLower());
            }));
        }
        private void UpdateView()
        {
            var categories = Accounts.Select(a => a.Category).ToList();
            Categories = new ObservableCollection<string>(categories.Distinct().ToList());
            ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
        }
    }
}
