using System.Collections.Generic;
using SecurePass.Models;

namespace SecurePass.Repositories.Interfaces
{
    public interface INotesRepository
    {
        IEnumerable<Note> GetNotes();
        Note GetNote(int id);
        void SetNote(Note note);
        void RemoveNote(Note note);
        void AddNote(Note note);
    }
}