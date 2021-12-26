using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using SecurePass.SQLite;

namespace SecurePass.Services
{
    public class UserAuthentication
    {
        public bool Authentication(SecureString password, string name)
        {
            bool isPasswordCorrect = IsPasswordCorrect(password, name);

            if(isPasswordCorrect)
            {
                using var context = new DatabaseContext(password, name);
                context.Database.EnsureCreated();
            }

            return isPasswordCorrect;
        }

        private bool IsPasswordCorrect(SecureString password, string name)
        {        
            bool result;
            
            if (File.Exists($"{name}mcode"))
            {
                var encryption = new EncryptionPassword();
                var mcode = new NetworkCredential("",
                    encryption.Decrypt(File.ReadAllText($"{name}mcode"))).SecurePassword;

                result = PasswordEqual(password, mcode);
            }
            else
            {
                var encryption = new EncryptionPassword();
                using (var sw = File.CreateText($"{name}mcode"))
                {
                    sw.Write(encryption.Encrypt(new NetworkCredential("", password).Password));
                }

                result = true;
            }
            
            return result;
        }

        private bool PasswordEqual(SecureString ss1, SecureString ss2)
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