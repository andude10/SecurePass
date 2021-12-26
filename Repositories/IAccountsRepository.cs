using System;
using System.Collections.Generic;
using SecurePass.Models;

namespace SecurePass.Repositories
{
    public interface IAccountsRepository : IDisposable
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int id);
        void SetAccount(Account newAccount, string oldPass);
        void RemoveAccount(Account account);
        void AddAccount(Account account);
    }
}