using SecurePass.ViewModels;
using System.Windows.Controls;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryUC : UserControl
    {
        public HistoryUC()
        {
            InitializeComponent();
            DataContext = new HistoryVM();
        }
    }
}
