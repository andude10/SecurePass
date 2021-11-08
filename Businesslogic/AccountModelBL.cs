using Microsoft.EntityFrameworkCore;
using SecurePass.Models;
using SecurePass.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace SecurePass.Businesslogic
{
    public static class AccountModelBL
    {
        public static bool IsPasswordCorrect(SecureString password, string name)
        {
            using AccountsContext context = new AccountsContext(password, name);
            if (AccountsContext.SuccessfulRegistration)
            {
                context.Database.EnsureCreated();
            }
            return AccountsContext.SuccessfulRegistration;
        }
        public static void SaveChanges()
        {
            using AccountsContext context = new AccountsContext();
            context.SaveChanges();
        }
        public static IEnumerable<Account> GetAccounts()
        {
            using AccountsContext context = new AccountsContext();
            return context.Accounts.ToList();
        }
        public static Account GetAccount(int id)
        {
            using AccountsContext context = new AccountsContext();
            return context.Accounts.Find(id);
        }
        public static void SetAccount(Account value)
        {
            using AccountsContext context = new AccountsContext();
            context.Accounts.Update(value);
            context.SaveChanges();
        }
        public static void RemoveAccount(Account account)
        {
            using AccountsContext context = new AccountsContext();
            context.Accounts.Remove(account);
            context.SaveChanges();
        }

        public static void AddAccount(Account account)
        {
            using AccountsContext context = new AccountsContext();
            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }
}
