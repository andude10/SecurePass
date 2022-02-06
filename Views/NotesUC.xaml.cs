using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;
using SecurePass.Views.Windows;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesUC
    {
        public NotesUC()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<NotesUC, NewNoteWindowMessage>(this, NewNote);
            WeakReferenceMessenger.Default.Register<NotesUC, EditNoteWindowMessage>(this, EditNote);
            DataContext = ViewModelManager.NotesVM;
        }

        private static void NewNote(NotesUC recipient, NewNoteWindowMessage message)
        {
            var newNoteWindow = new NewNoteWindow();
            if (newNoteWindow.ShowDialog() == true) message.Reply(newNoteWindow.NewNote);
        }

        private static void EditNote(NotesUC recipient, EditNoteWindowMessage message)
        {
            var editNoteWindow = new EditNoteWindow(message.Note);
            if (editNoteWindow.ShowDialog() == true) message.Reply(editNoteWindow.EditNote);
        }
    }
}