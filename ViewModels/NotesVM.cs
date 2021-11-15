using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Businesslogic;
using SecurePass.Models;
using SecurePass.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurePass.ViewModels
{
    public class NotesVM : BaseViewModel
    {
        public NotesVM()
        {
            Notes = AccountModelBL.GetNotes().ToList();
            ActualNotes = new ObservableCollection<Note>(Notes);
        }

        private List<Note> _notes;
        public List<Note> Notes
        {
            get { return _notes; }
            set { RaisePropertyChanged(ref _notes, value); }
        }
        private ObservableCollection<Note> _actualNotes;
        public ObservableCollection<Note> ActualNotes
        {
            get { return _actualNotes; }
            set { RaisePropertyChanged(ref _actualNotes, value); }
        }

        #region Commands
        private ICommand _addNote;
        public ICommand AddNote
        {
            get
            {
                return _addNote ??= new RelayCommand(() =>
                {
                    Note newNote;
                    try
                    {
                        newNote = WeakReferenceMessenger.Default.Send<NewNoteWindowMessage>();
                    }
                    catch (System.InvalidOperationException)
                    { return; }
                    newNote.Date = DateTime.Now.ToString();
                    AccountModelBL.AddNote(newNote);
                    UpdateView();
                });
            }
        }
        private ICommand _editNote;
        public ICommand EditNote
        {
            get
            {
                return _editNote ??= new RelayCommand<int>(obj =>
                {
                    int id = obj;
                    Note note = Notes.Find(a => a.NoteId == id);
                    try
                    {
                        EditNoteWindowMessage editWindow = new EditNoteWindowMessage(note);
                        note = WeakReferenceMessenger.Default.Send(editWindow);
                    }
                    catch (System.InvalidOperationException)
                    { return; }
                    AccountModelBL.SetNote(note);
                    UpdateView();
                });
            }
        }
        private ICommand _deleteNote;
        public ICommand DeleteNote
        {
            get
            {
                return _deleteNote ??= new RelayCommand<int>(obj =>
                {
                    int id = obj;
                    Note note = Notes.Find(a => a.NoteId == id);
                    AccountModelBL.RemoveNote(note);
                    UpdateView();
                });
            }
        }
        #endregion
        public override void SearchingData(string enteredText)
        {
            ActualNotes = new ObservableCollection<Note>(Notes.Where(n =>
            {
                var alltext = n.Text + n.Title + n.Date;
                return alltext.ToLower().Contains(enteredText.ToLower());
            }));
        }
        private void UpdateView()
        {
            Notes = AccountModelBL.GetNotes().ToList();
            ActualNotes = new ObservableCollection<Note>(Notes);
        }
    }
}
