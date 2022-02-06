using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SecurePass.Models;

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