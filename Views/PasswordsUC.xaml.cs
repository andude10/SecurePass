using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for PasswordsPage.xaml
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
            var newAccountDialog = new NewAccountDialog();
            if (newAccountDialog.ShowDialog() == true) message.Reply(newAccountDialog.NewAccount);
        }

        private static void EditAccount(PasswordsUC recipient, EditAccountWindowMessage message)
        {
            var editAccountDialog = new EditAccountDialog(message.Account);
            if (editAccountDialog.ShowDialog() == true) message.Reply(editAccountDialog.EditAccount);
        }
    }
}