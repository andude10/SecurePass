using System;
using System.Collections.Generic;
using SecurePass.Models;

namespace SecurePass.Repositories
{
    public interface INotesRepository : IDisposable
    {
         IEnumerable<Note> GetNotes();
         Note GetNote(int id);
         void SetNote(Note note);
         void RemoveNote(Note note);
         void AddNote(Note note);
    }
}