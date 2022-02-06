using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SecurePass.Models;

namespace SecurePass.Views.Windows
{
    /// <summary>
    ///     Interaction logic forEditAccountDialog.xaml
    /// </summary>
    public partial class EditAccountDialog : INotifyPropertyChanged
    {
        private Account _editAccount;

        public EditAccountDialog(Account editAccount)
        {
            InitializeComponent();
            EditAccount = editAccount;
            DataContext = EditAccount;

            // Get MainWindow
            Owner = Application.Current.Windows[0];
        }

        public Account EditAccount
        {
            get => _editAccount;
            set => RaisePropertyChanged(ref _editAccount, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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

        private void ReadyAccountClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}