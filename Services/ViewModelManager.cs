using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurePass.ViewModels;

namespace SecurePass.Services
{
    public static class ViewModelManager
    {
        public static LoginVM LoginVM { get; set; }
        public static MainVM MainVM { get; set; }
        public static AccountsVM AccountsVM { get; set; }
        public static HistoryVM HistoryVM { get; set; }
        public static NotesVM NotesVM { get; set; }
    }
}
