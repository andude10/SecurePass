using Microsoft.EntityFrameworkCore;
using SecurePass.SQLite;
using SecurePass.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SecurePass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountsVM AccountsVM = new AccountsVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = AccountsVM;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }
}
