using SecurePass.Services;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryUC
    {
        public HistoryUC()
        {
            InitializeComponent();
            DataContext = ViewModelManager.HistoryVM;
        }
    }
}