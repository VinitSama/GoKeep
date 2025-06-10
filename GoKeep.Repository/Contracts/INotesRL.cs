using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public interface INotesRL
    {
        Task<List<GetAllNotesResponseModel>> GetAllNotes(string email);
        Task<int> AddNotes(string email, AddNoteRequestModel noteModel);
        Task<bool> DeleteNotes(int noteId);
        Task<bool> UpdateNotes(int noteId, UpdateNoteRequestModel noteModel);
        Task<bool> ToggleTrashNote(int noteId);
        Task<bool> TogglePinNote(int noteId);
        Task<bool> ToggleArchiveNote(int noteId);
        Task<bool> DeleteForever(int noteId);
    }
}
