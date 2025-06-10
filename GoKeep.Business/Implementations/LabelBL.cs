using GoKeep.Model;
using GoKeep.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL _labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            _labelRL = labelRL;
        }

        public async Task<int> CreateNewLabel(string email, CreateLabelRequestModel labelModel)
        {
            try
            {
                return await _labelRL.CreateNewLabel(email, labelModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLabel(int labelId)
        {
            try
            {
                return await _labelRL.DeleteLabel(labelId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllLabelResponseModel>> GetAllLabel(string email)
        {
            try
            {
                return await _labelRL.GetAllLabel(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateLabel(UpdateLabelRequestModel labelModel)
        {
            try
            {
                return await _labelRL.UpdateLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
