using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.Models
{
    public class APasswordChange
    {
        public APasswordChange(string change)
        {
            Change = change;
            AllChenges.Add(Change);
        }
        public string Change { get; set; }
        public static List<string> AllChenges { get; set; } = new List<string>();
    }
}
