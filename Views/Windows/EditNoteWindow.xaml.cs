using SecurePass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window, INotifyPropertyChanged
    {
        private Note _editNote;
        public Note EditNote
        {
            get { return _editNote; }
            set { RaisePropertyChanged(ref _editNote, value); }
        }
        public EditNoteWindow(Note editnote)
        {
            InitializeComponent();
            EditNote = editnote;
            DataContext = EditNote;
            Owner = App.Current.MainWindow;
        }
        private void CloseWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: this.DialogResult = true; this.Close(); break;
                case Key.Enter: this.DialogResult = true; this.Close(); break;
            }
        }
        private void ReadyNoteClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
