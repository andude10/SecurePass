using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Repositories;
using SecurePass.Models;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class AccountsVM : BaseViewModel
    {
        private List<Account> _accounts;
        private ObservableCollection<Account> _actualAccounts;
        private ObservableCollection<string> _categories;
        private IAccountsRepository _accountsRepository;

        private string _selectedCategory;

        public AccountsVM()
        {
            _accountsRepository = new AccountsRepository(new AccountsChangesRepository());
            Accounts = _accountsRepository.GetAccounts().ToList();
            Categories = new ObservableCollection<string>();
            ActualAccounts = new ObservableCollection<Account>(Accounts);
            UpdateView();
        }

        public List<Account> Accounts
        {
            get => _accounts;
            set => RaisePropertyChanged(ref _accounts, value);
        }

        public ObservableCollection<Account> ActualAccounts
        {
            get => _actualAccounts;
            set => RaisePropertyChanged(ref _actualAccounts, value);
        }

        public ObservableCollection<string> Categories
        {
            get => _categories;
            set => RaisePropertyChanged(ref _categories, value);
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                RaisePropertyChanged(ref _selectedCategory, value);
                ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
            }
        }

        public override void SearchingData(string enteredText)
        {
            ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a =>
            {
                var allText = a.Url + a.Username;
                return allText.ToLower().Contains(enteredText.ToLower());
            }));
        }

        private void UpdateView()
        {
            var categories = Accounts.Select(a => a.Category).ToList();
            Categories = new ObservableCollection<string>(categories.Distinct().ToList());
            ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
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
                    catch (InvalidOperationException)
                    {
                        return;
                    }

                    newAccount.Category ??= "No category";

                    _accountsRepository.AddAccount(newAccount);
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
                    var id = obj;
                    var account = Accounts.Find(a => a.AccountId == id);
                    if (account != null)
                    {
                        var oldPass = account.Password;
                        try
                        {
                            var editWindow = new EditAccountWindowMessage(account);
                            account = WeakReferenceMessenger.Default.Send(editWindow);
                        }
                        catch (InvalidOperationException)
                        {
                            return;
                        }

                        account.Category ??= "No category";
                        _accountsRepository.SetAccount(account, oldPass);
                    }

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
                    var id = obj;
                    var account = Accounts.Find(a => a.AccountId == id);

                    Accounts.Remove(account);
                    _accountsRepository.RemoveAccount(account);

                    UpdateView();
                });
            }
        }

        private ICommand _allPassword;

        public ICommand AllPassword
        {
            get
            {
                return _allPassword ??= new RelayCommand(() =>
                {
                    ActualAccounts = new ObservableCollection<Account>(Accounts);
                });
            }
        }

        private ICommand _copyPassword;

        public ICommand CopyPassword
        {
            get
            {
                return _copyPassword ??= new RelayCommand<int>(obj =>
                {
                    var id = obj;
                    Clipboard.SetText(Accounts.Find(ch => ch.AccountId == id).Password);
                });
            }
        }

        #endregion
    }
}