using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public interface INotesBL
    {
        Task<List<GetAllNotesResponseModel>> GetAllNotes(string email);
        Task<int> AddNewNote(string email, AddNoteRequestModel noteModel);
        Task<bool> DeleteNote(int noteId);
        Task<bool> UpdateNote(int noteId, UpdateNoteRequestModel noteModel);
        Task<bool> ToggleTrashNote(int noteId);
        Task<bool> TogglePinNote(int noteId);
        Task<bool> ToggleArchiveNote(int noteId);
        Task<bool> DeleteForever(int noteId);
    }
}
