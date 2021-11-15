using SecurePass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class NewAccountDialog : Window, INotifyPropertyChanged
    {
        private Account _newAccount;
        public Account NewAccount
        {
            get { return _newAccount; }
            set { RaisePropertyChanged(ref _newAccount, value); }
        }
        public NewAccountDialog()
        {
            InitializeComponent();
            NewAccount = new Account();
            DataContext = NewAccount;
            Owner = App.Current.MainWindow;
        }
        private void CloseWindow(object sender, System.Windows.RoutedEventArgs e) => this.Close();
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
        private void GeneratePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneratePasswordWindow generatePassword = new GeneratePasswordWindow();
            if (generatePassword.ShowDialog() == true) 
            {
                NewAccount.Password = generatePassword.Password;
                passwordTextBox.Text = generatePassword.Password;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
