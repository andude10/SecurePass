using SecurePass.Services;
using SecurePass.ViewModels;
using System.Windows.Controls;

namespace SecurePass.Views
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryUC : UserControl
    {
        public HistoryUC()
        {
            InitializeComponent();
            DataContext = ViewModelManager.HistoryVM;
        }
    }
}
