using System.Windows;
using System.Windows.Input;
using SecurePass.Models;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for NewNoteWindow.xaml
    /// </summary>
    public partial class NewNoteWindow : Window
    {
        public Note NewNote = new();

        public NewNoteWindow()
        {
            InitializeComponent();
            DataContext = NewNote;
            Owner = Application.Current.MainWindow;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    DialogResult = true;
                    Close();
                    break;
                case Key.Enter:
                    DialogResult = true;
                    Close();
                    break;
            }
        }

        private void ReadyNoteClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}