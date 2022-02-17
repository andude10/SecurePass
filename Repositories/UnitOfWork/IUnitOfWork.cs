using System;
using SecurePass.Repositories.Interfaces;

namespace SecurePass.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountsRepository AccountsRepository { get; }
        IAccountsChangesRepository AccountsChangesRepository { get; }
        INotesRepository NotesRepository { get; }
        void Save();
    }
}