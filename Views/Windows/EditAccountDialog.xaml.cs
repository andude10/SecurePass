using SecurePass.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для EditAccountDialog.xaml
    /// </summary>
    public partial class EditAccountDialog : Window, INotifyPropertyChanged
    {
        private Account _editAccount;
        public Account EditAccount
        {
            get { return _editAccount; }
            set { RaisePropertyChanged(ref _editAccount, value); }
        }
        public EditAccountDialog(Account editAccount)
        {
            InitializeComponent();
            EditAccount = editAccount;
            DataContext = EditAccount;
            Owner = App.Current.MainWindow;
        }
        private void CloseWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: this.DialogResult = true; this.Close(); break;
                case Key.Enter: this.DialogResult = true; this.Close(); break;
            }
        }
        private void ReadyAccountClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
