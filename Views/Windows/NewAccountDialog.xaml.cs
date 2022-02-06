using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SecurePass.Models;

namespace SecurePass.Views.Windows
{
    /// <summary>
    ///     Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class NewAccountDialog : INotifyPropertyChanged
    {
        private Account _newAccount;

        public NewAccountDialog()
        {
            InitializeComponent();
            NewAccount = new Account();
            DataContext = NewAccount;

            // Get MainWindow
            Owner = Application.Current.Windows[0];
        }

        public Account NewAccount
        {
            get => _newAccount;
            set => RaisePropertyChanged(ref _newAccount, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    DialogResult = true;
                    Close();
                    break;
                case Key.Enter:
                    DialogResult = true;
                    Close();
                    break;
            }
        }

        private void AddAccountClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            var generatePassword = new GeneratePasswordWindow();
            if (generatePassword.ShowDialog() == true)
            {
                NewAccount.Password = generatePassword.Password;
                passwordTextBox.Text = generatePassword.Password;
            }
        }

        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}