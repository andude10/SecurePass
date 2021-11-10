using Microsoft.EntityFrameworkCore;
using SecurePass.SQLite;
using SecurePass.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SecurePass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
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
    }
}
