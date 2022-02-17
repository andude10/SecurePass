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
        private LoginVm LoginVm { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            raw.SetProvider(new SQLite3Provider_e_sqlcipher());

            LoginVm = new LoginVm();
            LoginWindow = new LoginWindow {DataContext = LoginVm};
            LoginWindow.Show();
        }
    }
}