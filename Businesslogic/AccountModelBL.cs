using Microsoft.EntityFrameworkCore;
using SecurePass.Models;
using SecurePass.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurePass.Businesslogic
{
    public static class AccountModelBL
    {
        public static string Password;

        public static bool IsPasswordCorrect(string password)
        {
            Password = password;
            using AccountsContext context = new AccountsContext(Password);
            if (context.SuccessfulRegistration)
            {
                context.Database.EnsureCreated();
            }
            return context.SuccessfulRegistration;
        }
        public static void SaveChanges()
        {
            using AccountsContext context = new AccountsContext(Password);
            context.SaveChanges();
        }
        public static IEnumerable<Account> GetAccounts()
        {
            using AccountsContext context = new AccountsContext(Password);
            return context.Accounts.ToList();
        }
        public static Account GetAccount(int id)
        {
            using AccountsContext context = new AccountsContext(Password);
            return context.Accounts.Find(id);
        }
        public static void SetAccount(Account value)
        {
            using AccountsContext context = new AccountsContext(Password);
            context.Accounts.Update(value);
            context.SaveChanges();
        }
        public static void RemoveAccount(Account account)
        {
            using AccountsContext context = new AccountsContext(Password);
            context.Accounts.Remove(account);
            context.SaveChanges();
        }

        public static void AddAccount(Account account)
        {
            using AccountsContext context = new AccountsContext(Password);
            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }
}
