using GoKeep.Model;
using GoKeep.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public class NoteLableBL : INoteLableBL
    {
        private readonly INoteLabelRL _noteLabelRL;
        public NoteLableBL(INoteLabelRL noteLabelRL)
        {
            _noteLabelRL = noteLabelRL;
        }

        public async Task<bool> AddNoteLabel(NoteLabelRequestModel notelabelModel, string email)
        {
            try
            {
                return await _noteLabelRL.AddNoteLabel(notelabelModel, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteNoteLabel(NoteLabelRequestModel notelabelModel)
        {
            try
            {
                return await _noteLabelRL.DeleteNoteLabel(notelabelModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetLabelIdByNoteIdResponseModel>> GetLabelIdByNoteIds(int noteId)
        {
            try
            {
                return await _noteLabelRL.GetLabelIdByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllNoteIdByLabelIdModel>> GetNoteIdByLabelId(int labelID)
        {
            try
            {
                return await _noteLabelRL.GetNoteIdByLabelId(labelID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetNotesLabelResponseModel>> GetNoteLable(string email)
        {
            try
            {
                return await _noteLabelRL.GetNoteLable(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
