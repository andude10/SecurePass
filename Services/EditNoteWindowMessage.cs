using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SecurePass.Models;

namespace SecurePass.Services
{
    public class EditNoteWindowMessage : RequestMessage<Note>
    {
        public EditNoteWindowMessage(Note note)
        {
            Note = note;
        }

        public Note Note { get; set; }
    }
}