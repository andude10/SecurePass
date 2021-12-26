using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SecurePass.Models;

namespace SecurePass.SQLite
{
    public class DatabaseContext : DbContext
    {
        private static SecureString _password;
        public static string _name;

        public DatabaseContext()
        {
            if(_password == null || _name == null)
            {
               throw new ArgumentNullException("_password", "_name");
            }
        }

        public DatabaseContext(SecureString pass, string name)
        {
            _password = pass;
            _name = name;

            if(_password == null || _name == null)
            {
               throw new ArgumentNullException("_password", "_name");
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountChange> AccountsChanges { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = $"{_name}.db",
                Password = new NetworkCredential("", _password).Password
            }.ToString();

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}