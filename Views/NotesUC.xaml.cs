using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;
using SecurePass.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для NotesPage.xaml
    /// </summary>
    public partial class NotesUC : UserControl
    {
        public NotesUC()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<NotesUC, NewNoteWindowMessage>(this, NewNote);
            WeakReferenceMessenger.Default.Register<NotesUC, EditNoteWindowMessage>(this, EditNote);
            DataContext = new NotesVM();
        }

        private static void NewNote(NotesUC recipient, NewNoteWindowMessage message)
        {
            NewNoteWindow newNoteWindow = new NewNoteWindow();
            if (newNoteWindow.ShowDialog() == true)
            {
                message.Reply(newNoteWindow.NewNote);
            }
        }
        private static void EditNote(NotesUC recipient, EditNoteWindowMessage message)
        {
            EditNoteWindow editNoteWindow = new EditNoteWindow(message.Note);
            if (editNoteWindow.ShowDialog() == true)
            {
                message.Reply(editNoteWindow.EditNote);
            }
        }
    }
}
