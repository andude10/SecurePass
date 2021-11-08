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
        public static void SetAccount(Account newaccount, string oldpass)
        {
            using AccountsContext context = new AccountsContext();
            if(oldpass != newaccount.Password)
            {
                AddAccountChanges(newaccount.Username, newaccount.Url, oldpass, newaccount.Password);
            }
            
            context.Accounts.Update(newaccount);
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


        public static IEnumerable<AccountChange> GetAccountsChanges()
        {
            using AccountsContext context = new AccountsContext();
            return context.AccountsChanges.ToList();
        }
        public static AccountChange GetAccountChanges(int id)
        {
            using AccountsContext context = new AccountsContext();
            return context.AccountsChanges.Find(id);
        }
        public static void AddAccountChanges(string username, string url, string oldpass, string newpass)
        {
            using AccountsContext context = new AccountsContext();
            string message = $"Account password with username '{username}' on '{url}' website changed from '{oldpass}' to '{newpass}'";
            context.AccountsChanges.Add(new AccountChange() { Change = message, Date = DateTime.Now.ToString()});
            context.SaveChanges();
        }
    }
}
