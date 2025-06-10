using GoKeep.Model;
using GoKeep.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL _notesRL;
        public NotesBL(INotesRL notesRL)
        {
            _notesRL = notesRL;
        }

        public async Task<int> AddNewNote(string email, AddNoteRequestModel noteModel)
        {
            try
            {
                return await _notesRL.AddNotes(email, noteModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteForever(int noteId)
        {
            try
            {
                return await _notesRL.DeleteForever(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteNote(int noteId)
        {
            try
            {
                return await _notesRL.DeleteNotes(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllNotesResponseModel>> GetAllNotes(string email)
        {
            try
            {
                return await _notesRL.GetAllNotes(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ToggleArchiveNote(int noteId)
        {
            try
            {
                return await _notesRL.ToggleArchiveNote(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TogglePinNote(int noteId)
        {
            try
            {
                return await _notesRL.TogglePinNote(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ToggleTrashNote(int noteId)
        {
            try
            {
                return await _notesRL.ToggleTrashNote(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(int noteId, UpdateNoteRequestModel noteModel)
        {
            try
            {
                return await _notesRL.UpdateNotes(noteId, noteModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
