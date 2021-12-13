using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.Services
{
    public class LoginWindowMessage
    {
        public LoginWindowMessage(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
        public bool IsSuccessful { get; set; }
    }
}
