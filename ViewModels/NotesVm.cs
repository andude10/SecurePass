using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Models;
using SecurePass.Repositories.UnitOfWork;
using SecurePass.Services;

namespace SecurePass.ViewModels
{
    public class NotesVm : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private ObservableCollection<Note> _actualNotes;
        private List<Note> _notes;

        public NotesVm(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            Notes = _unitOfWork.NotesRepository.GetNotes().ToList();
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

        //TODO: Remove this crutch
        private void UpdateView()
        {
            Notes = _unitOfWork.NotesRepository.GetNotes().ToList();
            ActualNotes = new ObservableCollection<Note>(Notes);
        }

        #region Commands

        private ICommand _addNote;

        public ICommand AddNote => _addNote ??= new RelayCommand(() =>
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
            _unitOfWork.NotesRepository.AddNote(newNote);
            _unitOfWork.Save();
            UpdateView();
        });

        private ICommand _editNote;

        public ICommand EditNote => _editNote ??= new RelayCommand<int>(obj =>
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

            _unitOfWork.NotesRepository.SetNote(note);
            _unitOfWork.Save();
            UpdateView();
        });

        private ICommand _deleteNote;

        public ICommand DeleteNote => _deleteNote ??= new RelayCommand<int>(obj =>
        {
            var id = obj;
            var note = Notes.Find(a => a.NoteId == id);
            _unitOfWork.NotesRepository.RemoveNote(note);
            _unitOfWork.Save();
            UpdateView();
        });

        #endregion
    }
}