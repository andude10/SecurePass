using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using SecurePass.Models;
using SecurePass.SQLite;

namespace SecurePass.Businesslogic
{
    public static class AccountModelBL
    {
        public static bool IsPasswordCorrect(SecureString password, string name)
        {
            using var context = new AccountsContext(password, name);
            if (AccountsContext.SuccessfulRegistration) context.Database.EnsureCreated();
            return AccountsContext.SuccessfulRegistration;
        }

        public static void SaveChanges()
        {
            using var context = new AccountsContext();
            context.SaveChanges();
        }

        #region Account

        public static IEnumerable<Account> GetAccounts()
        {
            using var context = new AccountsContext();
            return context.Accounts.ToList();
        }

        public static Account GetAccount(int id)
        {
            using var context = new AccountsContext();
            return context.Accounts.Find(id);
        }

        public static void SetAccount(Account newAccount, string oldPass)
        {
            using var context = new AccountsContext();
            if (oldPass != newAccount.Password)
                AddAccountChanges(newAccount.Username, newAccount.Url, oldPass, newAccount.Password);

            context.Accounts.Update(newAccount);
            context.SaveChanges();
        }

        public static void RemoveAccount(Account account)
        {
            using var context = new AccountsContext();
            context.Accounts.Remove(account);
            context.SaveChanges();
        }

        public static void AddAccount(Account account)
        {
            using var context = new AccountsContext();
            context.Accounts.Add(account);
            context.SaveChanges();
        }

        #endregion

        #region AccountChange

        public static IEnumerable<AccountChange> GetAccountsChanges()
        {
            using var context = new AccountsContext();
            return context.AccountsChanges.ToList();
        }

        public static AccountChange GetAccountChanges(int id)
        {
            using var context = new AccountsContext();
            return context.AccountsChanges.Find(id);
        }

        public static void AddAccountChanges(string username, string url, string oldPass, string newPass)
        {
            using var context = new AccountsContext();
            var message =
                $"Account password with username '{username}' on '{url}' website changed from '{oldPass}' to '{newPass}'";
            context.AccountsChanges.Add(new AccountChange {Change = message, Date = DateTime.Now.ToString(CultureInfo.InvariantCulture)});
            context.SaveChanges();
        }

        #endregion

        #region Note

        public static IEnumerable<Note> GetNotes()
        {
            using var context = new AccountsContext();
            return context.Notes.ToList();
        }

        public static Note GetNote(int id)
        {
            using var context = new AccountsContext();
            return context.Notes.Find(id);
        }

        public static void SetNote(Note note)
        {
            using var context = new AccountsContext();
            context.Notes.Update(note);
            context.SaveChanges();
        }

        public static void RemoveNote(Note note)
        {
            using var context = new AccountsContext();
            context.Notes.Remove(note);
            context.SaveChanges();
        }

        public static void AddNote(Note note)
        {
            using var context = new AccountsContext();
            context.Notes.Add(note);
            context.SaveChanges();
        }

        #endregion
    }
}