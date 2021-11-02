using SecurePass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для AddAccount.xaml
    /// </summary>
    public partial class NewAccountDialog : Window
    {
        public Account NewAccount = new Account();
        public NewAccountDialog()
        {
            InitializeComponent();
            DataContext = NewAccount;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: this.DialogResult = true; this.Close(); break;
                case Key.Enter: this.DialogResult = true; this.Close(); break;
            }
        }
        private void AddAccountClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
