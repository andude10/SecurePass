using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SecurePass.Models;
using SecurePass.Repositories.Interfaces;
using SecurePass.SQLite;

namespace SecurePass.Repositories.Implementations
{
    public sealed class AccountsChangesRepository : IAccountsChangesRepository
    {
        public IEnumerable<AccountChange> GetAccountsChanges()
        {
            using var context = new DatabaseContext();
            return context.AccountsChanges.ToList();
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
            context.AccountsChanges.Add(new AccountChange
                {Change = message, Date = DateTime.Now.ToString(CultureInfo.InvariantCulture)});
            context.SaveChanges();
        }
    }
}