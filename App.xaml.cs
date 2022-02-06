using System.Windows;
using SecurePass.ViewModels;
using SecurePass.Views.Windows;
using SQLitePCL;

namespace SecurePass
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private LoginWindow LoginWindow { get; set; }
        private LoginVM LoginVM { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            raw.SetProvider(new SQLite3Provider_e_sqlcipher());

            LoginVM = new LoginVM();
            LoginWindow = new LoginWindow {DataContext = LoginVM};
            LoginWindow.Show();
        }
    }
}