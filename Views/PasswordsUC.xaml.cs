using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;
using SecurePass.ViewModels;
using System.Windows.Controls;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для PasswordsPage.xaml
    /// </summary>
    public partial class PasswordsUC : UserControl
    {
        public PasswordsUC()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<PasswordsUC, NewAccountWindowMessage>(this, NewAccount);
            WeakReferenceMessenger.Default.Register<PasswordsUC, EditAccountWindowMessage>(this, EditAccount);
            DataContext = new AccountsVM();
        }

        private static void NewAccount(PasswordsUC recipient, NewAccountWindowMessage message)
        {
            NewAccountDialog newAccountDialog = new NewAccountDialog();
            newAccountDialog.Owner = App.Current.MainWindow;
            if (newAccountDialog.ShowDialog() == true)
            {
                message.Reply(newAccountDialog.NewAccount);
            }
        }
        private static void EditAccount(PasswordsUC recipient, EditAccountWindowMessage message)
        {
            EditAccountDialog editAccountDialog = new EditAccountDialog(message.Account);
            editAccountDialog.Owner = App.Current.MainWindow;
            if (editAccountDialog.ShowDialog() == true)
            {
                message.Reply(editAccountDialog.EditAccount);
            }
        }

        private void GeneratePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneratePasswordWindow generatePassword = new GeneratePasswordWindow();
            generatePassword.Owner = App.Current.MainWindow;
            generatePassword.ShowDialog();
        }
    }
}
