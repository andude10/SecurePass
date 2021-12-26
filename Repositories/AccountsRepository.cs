using System.Collections.Generic;
using SecurePass.Models;
using SecurePass.SQLite;

namespace SecurePass.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private DatabaseContext _dbContext;
        private IAccountsChangesRepository _accountsChangesRepository;

        public AccountsRepository(IAccountsChangesRepository accountsChangesRepository)
        {
            _dbContext = new DatabaseContext();
            _accountsChangesRepository = accountsChangesRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _dbContext.Accounts;
        }

        public Account GetAccount(int id)
        {
            return _dbContext.Accounts.Find(id);
        }

        public void SetAccount(Account newAccount, string oldPass)
        {            
            if (oldPass != newAccount.Password)
            {
                _accountsChangesRepository.AddAccountChanges(newAccount.Username, newAccount.Url, oldPass, newAccount.Password);
            }

            _dbContext.Accounts.Update(newAccount);
            _dbContext.SaveChanges();
        }

        public void RemoveAccount(Account account)
        {
            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }

        public void AddAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}