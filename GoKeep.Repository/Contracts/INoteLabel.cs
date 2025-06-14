using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public interface INoteLabelRL
    {
        Task<List<GetNotesLabelResponseModel>> GetNoteLable(string email);
        Task<bool> AddNoteLabel(NoteLabelRequestModel noteLabelModel,string email);
        Task<bool> DeleteNoteLabel(NoteLabelRequestModel noteLabelModel);
        Task<List<GetLabelIdByNoteIdResponseModel>> GetLabelIdByNoteId(int noteId);
        Task<List<GetAllNoteIdByLabelIdModel>> GetNoteIdByLabelId(int labelID);
    }
}
