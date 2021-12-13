using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SecurePass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.Services
{
    public class EditAccountWindowMessage : RequestMessage<Account>
    {
        public EditAccountWindowMessage(Account account)
        {
            Account = account;
        }
        public Account Account { get; set; }
    }
}
