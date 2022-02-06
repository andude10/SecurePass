using System.Collections.Generic;
using System.Linq;
using SecurePass.Models;
using SecurePass.Repositories.Interfaces;
using SecurePass.SQLite;

namespace SecurePass.Repositories.Implementations
{
    public sealed class AccountsRepository : IAccountsRepository
    {
        private readonly IAccountsChangesRepository _changesRepository;

        public AccountsRepository(IAccountsChangesRepository changesRepository)
        {
            _changesRepository = changesRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            using var context = new DatabaseContext();
            return context.Accounts.ToList();
        }

        public Account GetAccount(int id)
        {
            using var context = new DatabaseContext();
            return context.Accounts.Find(id);
        }

        public void SetAccount(Account newAccount, string oldPass)
        {
            if (oldPass != newAccount.Password)
                _changesRepository.AddAccountChanges(newAccount.Username, newAccount.Url, oldPass, newAccount.Password);

            using var context = new DatabaseContext();
            context.Accounts.Update(newAccount);
            context.SaveChanges();
        }

        public void RemoveAccount(Account account)
        {
            using var context = new DatabaseContext();
            context.Accounts.Remove(account);
            context.SaveChanges();
        }

        public void AddAccount(Account account)
        {
            using var context = new DatabaseContext();
            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }
}