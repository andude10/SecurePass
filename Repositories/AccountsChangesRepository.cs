using System;
using System.Collections.Generic;
using System.Globalization;
using SecurePass.Models;
using SecurePass.SQLite;

namespace SecurePass.Repositories
{
    public class AccountsChangesRepository : IAccountsChangesRepository
    {
        private DatabaseContext _dbContext;

        public AccountsChangesRepository()
        {
            _dbContext = new DatabaseContext();
        }

        public IEnumerable<AccountChange> GetAccountsChanges()
        {
            using var context = new DatabaseContext();
            return context.AccountsChanges;
        }

        public AccountChange GetAccountChanges(int id)
        {
            using var context = new DatabaseContext();
            return context.AccountsChanges.Find(id);
        }

        public void AddAccountChanges(string username, string url, string oldPass, string newPass)
        {
            using var context = new DatabaseContext();
            var message =
                $"Account password with username '{username}' on '{url}' website changed from '{oldPass}' to '{newPass}'";
            context.AccountsChanges.Add(new AccountChange {Change = message, Date = DateTime.Now.ToString(CultureInfo.InvariantCulture)});
            context.SaveChanges();
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