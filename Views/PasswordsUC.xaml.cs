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
            DataContext = ViewModelManager.AccountsVM;
        }

        private static void NewAccount(PasswordsUC recipient, NewAccountWindowMessage message)
        {
            NewAccountDialog newAccountDialog = new NewAccountDialog();
            if (newAccountDialog.ShowDialog() == true)
            {
                message.Reply(newAccountDialog.NewAccount);
            }
        }
        private static void EditAccount(PasswordsUC recipient, EditAccountWindowMessage message)
        {
            EditAccountDialog editAccountDialog = new EditAccountDialog(message.Account);
            if (editAccountDialog.ShowDialog() == true)
            {
                message.Reply(editAccountDialog.EditAccount);
            }
        }
    }
}
