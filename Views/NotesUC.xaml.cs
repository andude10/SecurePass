using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Services;
using SecurePass.Views.Windows;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesUc
    {
        public NotesUc()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<NotesUc, NewNoteWindowMessage>(this, NewNote);
            WeakReferenceMessenger.Default.Register<NotesUc, EditNoteWindowMessage>(this, EditNote);
            DataContext = ViewModelManager.NotesVm;
        }

        private static void NewNote(NotesUc recipient, NewNoteWindowMessage message)
        {
            var newNoteWindow = new NewNoteWindow();
            if (newNoteWindow.ShowDialog() == true) message.Reply(newNoteWindow.NewNote);
        }

        private static void EditNote(NotesUc recipient, EditNoteWindowMessage message)
        {
            var editNoteWindow = new EditNoteWindow(message.Note);
            if (editNoteWindow.ShowDialog() == true) message.Reply(editNoteWindow.EditNote);
        }
    }
}