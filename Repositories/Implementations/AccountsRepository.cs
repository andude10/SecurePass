using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SecurePass.Data;
using SecurePass.Models;
using SecurePass.Repositories.Interfaces;

namespace SecurePass.Repositories.Implementations
{
    public sealed class AccountsRepository : IAccountsRepository
    {
        private readonly DatabaseContext _context;

        public AccountsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public Account GetAccount(int id)
        {
            return _context.Accounts.Find(id);
        }

        public void SetAccount(Account newAccount, string oldPass)
        {
            if (oldPass != newAccount.Password)
            {
                var message = $"Account password with username '{newAccount.Username}' on '{newAccount.Url}'" +
                              $" website changed from '{oldPass}' to '{newAccount.Password}'";

                _context.AccountsChanges.Add(new AccountChange
                {
                    Change = message,
                    Date = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                });
            }

            _context.Accounts.Update(newAccount);
        }

        public void RemoveAccount(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
        }
    }
}