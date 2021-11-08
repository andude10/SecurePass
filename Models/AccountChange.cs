using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.Models
{
    public class AccountChange
    {
        public int AccountChangeId { get; set; }
        public string Change { get; set; }
        public string Date { get; set; }
    }
}
