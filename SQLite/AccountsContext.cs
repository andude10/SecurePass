using Microsoft.EntityFrameworkCore;
using SecurePass.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Security;
using System;
using System.Runtime.InteropServices;
using System.Net;

namespace SecurePass.SQLite
{
    public class AccountsContext : DbContext
    {
        private static SecureString Password;
        public static bool SuccessfulRegistration;
        public static string Name;
        public DbSet<Account> Accounts { get; set; }
        public AccountsContext()
        { }
        public AccountsContext(SecureString password, string name = "dfuser")
        {
            Password = password;
            Name = name;
            if (File.Exists($"{Name}mcode"))
            {
                File.Decrypt($"{Name}mcode");
                SecureString mcode = new NetworkCredential("", File.ReadAllText($"{Name}mcode")).SecurePassword;
                File.Encrypt($"{Name}mcode");
                SuccessfulRegistration = PasswordEqual(password, mcode);
            }
            else
            {
                using (StreamWriter sw = File.CreateText($"{Name}mcode"))
                {
                    sw.Write(password);
                }
                File.Encrypt($"{Name}mcode");
                SuccessfulRegistration = true;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = $"{Name}.db",
                Password = new NetworkCredential("",Password).Password
            }.ToString();

            optionsBuilder.UseSqlite(connectionString);
        }
        public static bool PasswordEqual(SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
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
