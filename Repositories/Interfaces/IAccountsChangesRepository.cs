using System.Collections.Generic;
using SecurePass.Models;

namespace SecurePass.Repositories.Interfaces
{
    public interface IAccountsChangesRepository
    {
        IEnumerable<AccountChange> GetAccountsChanges();
        AccountChange GetAccountChanges(int id);
        void AddAccountChanges(string username, string url, string oldPass, string newPass);
    }
}