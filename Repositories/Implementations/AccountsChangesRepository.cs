using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SecurePass.Data;
using SecurePass.Models;
using SecurePass.Repositories.Interfaces;

namespace SecurePass.Repositories.Implementations
{
    public sealed class AccountsChangesRepository : IAccountsChangesRepository
    {
        private readonly DatabaseContext _context;

        public AccountsChangesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountChange> GetAccountsChanges()
        {
            return _context.AccountsChanges.ToList();
        }

        public AccountChange GetAccountChanges(int id)
        {
            return _context.AccountsChanges.Find(id);
        }

        public void AddAccountChanges(string username, string url, string oldPass, string newPass)
        {
            var message =
                $"Account password with username '{username}' on '{url}' website changed from '{oldPass}' to '{newPass}'";
            _context.AccountsChanges.Add(new AccountChange
                {Change = message, Date = DateTime.Now.ToString(CultureInfo.InvariantCulture)});
        }
    }
}