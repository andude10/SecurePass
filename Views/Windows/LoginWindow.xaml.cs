using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;
using SecurePass.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow Window { get; set; }
        public MainVM MainVM { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<LoginWindow, LoginWindowMessage>(this, Login);
            DataContext = new LoginVM();
        }
        private void Login(LoginWindow recipient, LoginWindowMessage message)
        {
            if(message.IsSuccessful)
            {
                MainVM = new MainVM();
                Window = new MainWindow { DataContext = MainVM };
                Window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong password entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Master_password = ((PasswordBox)sender).SecurePassword; }
        }
    }
}
