using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Models;
using SecurePass.Repositories.UnitOfWork;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class AccountsVm : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private List<Account> _accounts;
        private ObservableCollection<Account> _actualAccounts;
        private ObservableCollection<string> _categories;

        private string _selectedCategory;

        public AccountsVm(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Accounts = unitOfWork.AccountsRepository.GetAccounts().ToList();
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

        //TODO: Remove this crutch
        private void UpdateView()
        {
            var categories = Accounts.Select(a => a.Category).ToList();
            Categories = new ObservableCollection<string>(categories.Distinct().ToList());
            ActualAccounts = new ObservableCollection<Account>(Accounts.Where(a => a.Category == SelectedCategory));
        }

        #region Commands

        private ICommand _addAccount;

        public ICommand AddAccount => _addAccount ??= new RelayCommand(() =>
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

            _unitOfWork.AccountsRepository.AddAccount(newAccount);
            _unitOfWork.Save();
            Accounts.Add(newAccount);

            UpdateView();
        });

        private ICommand _editAccount;

        public ICommand EditAccount => _editAccount ??= new RelayCommand<int>(obj =>
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
                _unitOfWork.AccountsRepository.SetAccount(account, oldPass);
                _unitOfWork.Save();
            }

            UpdateView();
        });

        private ICommand _deleteAccount;

        public ICommand DeleteAccount => _deleteAccount ??= new RelayCommand<int>(obj =>
        {
            var id = obj;
            var account = Accounts.Find(a => a.AccountId == id);

            Accounts.Remove(account);
            _unitOfWork.AccountsRepository.RemoveAccount(account);
            _unitOfWork.Save();

            UpdateView();
        });

        private ICommand _allPassword;

        public ICommand AllPassword => _allPassword ??= new RelayCommand(() =>
        {
            ActualAccounts = new ObservableCollection<Account>(Accounts);
        });

        private ICommand _copyPassword;

        public ICommand CopyPassword => _copyPassword ??= new RelayCommand<int>(obj =>
        {
            var id = obj;
            if (Accounts == null) return;

            var password = Accounts.Find(ch => ch.AccountId == id)?.Password;
            if (password != null) Clipboard.SetText(password);
        });

        #endregion
    }
}