using Microsoft.EntityFrameworkCore;
using SecurePass.Models;
using Microsoft.Data.Sqlite;
using System.IO;

namespace SecurePass.SQLite
{
    public class AccountsContext : DbContext
    {
        private string _password;
        public bool SuccessfulRegistration;
        public DbSet<Account> Accounts { get; set; }
        public AccountsContext(string password)
        {
            _password = password;
            if (File.Exists("mcode"))
            {
                File.Decrypt("mcode");
                SuccessfulRegistration = password == File.ReadAllText("mcode");
                File.Encrypt("mcode");
            }
            else
            {
                using (StreamWriter sw = File.CreateText("mcode"))
                {
                    sw.Write(password);
                }
                File.Encrypt("mcode");
                SuccessfulRegistration = true;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = "Accounts.db",
                Password = _password
            }.ToString();

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
