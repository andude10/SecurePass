using System.Collections.Generic;
using System.Linq;
using SecurePass.Models;
using SecurePass.Repositories.Interfaces;
using SecurePass.SQLite;

namespace SecurePass.Repositories.Implementations
{
    public sealed class NotesRepository : INotesRepository
    {
        public IEnumerable<Note> GetNotes()
        {
            using var context = new DatabaseContext();
            return context.Notes.ToList();
        }

        public Note GetNote(int id)
        {
            using var context = new DatabaseContext();
            return context.Notes.Find(id);
        }

        public void SetNote(Note note)
        {
            using var context = new DatabaseContext();
            context.Notes.Update(note);
            context.SaveChanges();
        }

        public void RemoveNote(Note note)
        {
            using var context = new DatabaseContext();
            context.Notes.Remove(note);
            context.SaveChanges();
        }

        public void AddNote(Note note)
        {
            using var context = new DatabaseContext();
            context.Notes.Add(note);
            context.SaveChanges();
        }
    }
}