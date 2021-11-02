using Microsoft.EntityFrameworkCore;
using SecurePass.Models;
using System;

namespace SecurePass.SQLite
{
    public class AccountsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public AccountsContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite("Filename=Accounts.db");
        }
    }
}
