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
    public class AccountsContext : DbContext
    {
        private static SecureString Password;
        public static bool SuccessfulRegistration;
        public static string Name;

        public AccountsContext()
        {
        }

        public AccountsContext(SecureString password, string name = "dfuser")
        {
            Password = password;
            Name = name;
            if (File.Exists($"{Name}mcode"))
            {
                var encryption = new EncryptionPassword();
                var mcode = new NetworkCredential("",
                    encryption.Decrypt(File.ReadAllText($"{Name}mcode"))).SecurePassword;
                SuccessfulRegistration = PasswordEqual(password, mcode);
            }
            else
            {
                var encryption = new EncryptionPassword();
                using (var sw = File.CreateText($"{Name}mcode"))
                {
                    sw.Write(encryption.Encrypt(new NetworkCredential("", Password).Password));
                }

                SuccessfulRegistration = true;
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountChange> AccountsChanges { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = $"{Name}.db",
                Password = new NetworkCredential("", Password).Password
            }.ToString();

            optionsBuilder.UseSqlite(connectionString);
        }

        public static bool PasswordEqual(SecureString ss1, SecureString ss2)
        {
            var bstr1 = IntPtr.Zero;
            var bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                var length1 = Marshal.ReadInt32(bstr1, -4);
                var length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                    for (var x = 0; x < length1; ++x)
                    {
                        var b1 = Marshal.ReadByte(bstr1, x);
                        var b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                else return false;

                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }
    }
}