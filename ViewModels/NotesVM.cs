using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Businesslogic;
using SecurePass.Models;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class NotesVM : BaseViewModel
    {
        private ObservableCollection<Note> _actualNotes;

        private List<Note> _notes;

        public NotesVM()
        {
            Notes = AccountModelBL.GetNotes().ToList();
            ActualNotes = new ObservableCollection<Note>(Notes);
        }

        public List<Note> Notes
        {
            get => _notes;
            set => RaisePropertyChanged(ref _notes, value);
        }

        public ObservableCollection<Note> ActualNotes
        {
            get => _actualNotes;
            set => RaisePropertyChanged(ref _actualNotes, value);
        }

        public override void SearchingData(string enteredText)
        {
            ActualNotes = new ObservableCollection<Note>(Notes.Where(n =>
            {
                var allText = n.Text + n.Title + n.Date;
                return allText.ToLower().Contains(enteredText.ToLower());
            }));
        }

        private void UpdateView()
        {
            Notes = AccountModelBL.GetNotes().ToList();
            ActualNotes = new ObservableCollection<Note>(Notes);
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
                    catch (InvalidOperationException)
                    {
                        return;
                    }

                    newNote.Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
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
                    var id = obj;
                    var note = Notes.Find(a => a.NoteId == id);
                    try
                    {
                        var editWindow = new EditNoteWindowMessage(note);
                        note = WeakReferenceMessenger.Default.Send(editWindow);
                    }
                    catch (InvalidOperationException)
                    {
                        return;
                    }

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
                    var id = obj;
                    var note = Notes.Find(a => a.NoteId == id);
                    AccountModelBL.RemoveNote(note);
                    UpdateView();
                });
            }
        }

        #endregion
    }
}