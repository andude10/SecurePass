using Microsoft.EntityFrameworkCore;
using SecurePass.Models;
using SecurePass.SQLite;
using SecurePass.Utilities;
using SecurePass.Views;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SecurePass.Businesslogic;

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
        private Command _addAccount;
        public Command AddAccount
        {
            get
            {
                return _addAccount ??= new Command(obj =>
                    {
                        NewAccountDialog newAccountDialog = new NewAccountDialog();
                        bool? result = newAccountDialog.ShowDialog();
                        if (result == true)
                        {
                            AccountModelBL.AddAccount(newAccountDialog.NewAccount);
                            Accounts.Add(newAccountDialog.NewAccount);
                            UpdateView();
                        }
                    });
            }
        }
        private Command _editAccount;
        public Command EditAccount
        {
            get
            {
                return _editAccount ??= new Command(obj =>
                {
                    int id = (int)obj;
                    Account account = Accounts.Find(a => a.AccountId == id);
                    EditAccountDialog editAccountDialog = new EditAccountDialog(account);
                    bool? result = editAccountDialog.ShowDialog();
                    if (result == true)
                    {
                        account = editAccountDialog.EditAccount;
                        AccountModelBL.SetAccount(account);
                        UpdateView();
                    }
                });
            }
        }
        private Command _deleteAccount;
        public Command DeleteAccount
        {
            get
            {
                return _deleteAccount ??= new Command(obj =>
                {
                    int id = (int)obj;
                    Account account = Accounts.Find(a => a.AccountId == id);
                    Accounts.Remove(account);
                    AccountModelBL.RemoveAccount(account);
                    UpdateView();
                });
            }
        }
        private Command _save;
        public Command Save
        {
            get
            {
                return _save ??= new Command(obj =>
                {

                    Console.WriteLine("Save!");
                    AccountModelBL.SaveChanges();
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
