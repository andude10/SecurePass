using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SecurePass.Models;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window, INotifyPropertyChanged
    {
        private Note _editNote;

        public EditNoteWindow(Note editnote)
        {
            InitializeComponent();
            EditNote = editnote;
            DataContext = EditNote;
            Owner = Application.Current.MainWindow;
        }

        public Note EditNote
        {
            get => _editNote;
            set => RaisePropertyChanged(ref _editNote, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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

        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}