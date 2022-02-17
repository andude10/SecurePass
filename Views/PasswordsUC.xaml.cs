using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;
using SecurePass.Views.Windows;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for PasswordsPage.xaml
    /// </summary>
    public partial class PasswordsUc
    {
        public PasswordsUc()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<PasswordsUc, NewAccountWindowMessage>(this, NewAccount);
            WeakReferenceMessenger.Default.Register<PasswordsUc, EditAccountWindowMessage>(this, EditAccount);
            DataContext = ViewModelManager.AccountsVm;
        }

        private static void NewAccount(PasswordsUc recipient, NewAccountWindowMessage message)
        {
            var newAccountDialog = new NewAccountDialog();
            if (newAccountDialog.ShowDialog() == true) message.Reply(newAccountDialog.NewAccount);
        }

        private static void EditAccount(PasswordsUc recipient, EditAccountWindowMessage message)
        {
            var editAccountDialog = new EditAccountDialog(message.Account);
            if (editAccountDialog.ShowDialog() == true) message.Reply(editAccountDialog.EditAccount);
        }
    }
}