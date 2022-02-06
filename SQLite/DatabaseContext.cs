using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SecurePass.Models;

namespace SecurePass.SQLite
{
    public class DatabaseContext : DbContext
    {
        private static SecureString _password;
        private static string _name;

        public DatabaseContext()
        {
            if (_password == null || _name == null) throw new ArgumentNullException(nameof(DatabaseContext));
        }

        /// <summary>
        ///     This constructor must be called first to give value to _password and _name
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DatabaseContext([NotNull] SecureString pass, [NotNull] string name)
        {
            _password = pass;
            _name = name;
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