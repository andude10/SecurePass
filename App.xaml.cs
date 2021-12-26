using SecurePass.ViewModels;
using SecurePass.Views;
using System.Windows;

namespace SecurePass
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LoginWindow LoginWindow { get; set; }
        private LoginVM LoginVM { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlcipher());

            LoginVM = new LoginVM();
            LoginWindow = new LoginWindow() { DataContext = LoginVM };
            LoginWindow.Show();
        }
    }
}
