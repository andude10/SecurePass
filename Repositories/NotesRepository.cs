using System;
using System.Collections.Generic;
using SecurePass.Models;
using SecurePass.SQLite;

namespace SecurePass.Repositories
{
    public class NotesRepository : INotesRepository 
    {
        private DatabaseContext _dbContext;

        public NotesRepository()
        {
            _dbContext = new DatabaseContext();
        }

        public IEnumerable<Note> GetNotes()
        {
            return _dbContext.Notes;
        }

        public Note GetNote(int id)
        {
            return _dbContext.Notes.Find(id);
        }

        public void SetNote(Note note)
        {
            _dbContext.Notes.Update(note);
            _dbContext.SaveChanges();
        }

        public void RemoveNote(Note note)
        {
            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();
        }

        public void AddNote(Note note)
        {
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}