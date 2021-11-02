using SecurePass.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SecurePass
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow Window { get; set; }
        public MainVM MainVM { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainVM = new MainVM();
            Window = new MainWindow { DataContext = MainVM };
            Window.Show();
        }
    }
}
