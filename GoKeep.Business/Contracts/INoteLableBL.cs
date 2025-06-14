using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public interface INoteLableBL
    {
        Task<List<GetNotesLabelResponseModel>> GetNoteLable(string email);
        Task<bool> AddNoteLabel(NoteLabelRequestModel notelabelModel, string email);
        Task<bool> DeleteNoteLabel(NoteLabelRequestModel notelabelModel);
        Task<List<GetLabelIdByNoteIdResponseModel>> GetLabelIdByNoteIds(int noteId);
        Task<List<GetAllNoteIdByLabelIdModel>> GetNoteIdByLabelId(int labelID);
    }
}
