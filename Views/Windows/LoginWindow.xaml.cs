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
        public LoginWindow()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<LoginWindow, LoginWindowMessage>(this, Login);
            ViewModelManager.LoginVM = new LoginVM();
            DataContext = ViewModelManager.LoginVM;
        }
        private void Login(LoginWindow recipient, LoginWindowMessage message)
        {
            if(message.IsSuccessful)
            {
                ViewModelManager.MainVM = new MainVM();
                Window = new MainWindow();
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

        private void CloseWindow(object sender, System.Windows.RoutedEventArgs e) => this.Close();
        private void MinimizedWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaximizedWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;

        }
        private void MoveWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            DragMove();
        }
    }
}
