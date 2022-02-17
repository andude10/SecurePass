using System;
using SecurePass.Data;
using SecurePass.Repositories.Implementations;
using SecurePass.Repositories.Interfaces;

namespace SecurePass.Repositories.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context = new();

        public UnitOfWork()
        {
            AccountsRepository = new AccountsRepository(_context);
            AccountsChangesRepository = new AccountsChangesRepository(_context);
            NotesRepository = new NotesRepository(_context);
        }

        public IAccountsRepository AccountsRepository { get; }
        public IAccountsChangesRepository AccountsChangesRepository { get; }
        public INotesRepository NotesRepository { get; }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region Dispose interface

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}