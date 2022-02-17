using SecurePass.Services;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryUc
    {
        public HistoryUc()
        {
            InitializeComponent();
            DataContext = ViewModelManager.HistoryVm;
        }
    }
}