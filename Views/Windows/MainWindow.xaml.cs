using System.Windows;
using SecurePass.Services;

namespace SecurePass.Views.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModelManager.MainVm;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizedWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
    }
}