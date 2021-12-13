using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SecurePass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
