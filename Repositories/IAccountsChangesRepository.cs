using System;
using System.Collections.Generic;
using SecurePass.Models;

namespace SecurePass.Repositories
{
    public interface IAccountsChangesRepository : IDisposable
    {
         IEnumerable<AccountChange> GetAccountsChanges();
         AccountChange GetAccountChanges(int id);
         void AddAccountChanges(string username, string url, string oldPass, string newPass);
    }
}