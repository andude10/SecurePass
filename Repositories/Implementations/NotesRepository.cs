using System.Collections.Generic;
using System.Linq;
using SecurePass.Data;
using SecurePass.Models;
using SecurePass.Repositories.Interfaces;

namespace SecurePass.Repositories.Implementations
{
    public sealed class NotesRepository : INotesRepository
    {
        private readonly DatabaseContext _context;

        public NotesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes.ToList();
        }

        public Note GetNote(int id)
        {
            return _context.Notes.Find(id);
        }

        public void SetNote(Note note)
        {
            _context.Notes.Update(note);
        }

        public void RemoveNote(Note note)
        {
            _context.Notes.Remove(note);
        }

        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
        }
    }
}