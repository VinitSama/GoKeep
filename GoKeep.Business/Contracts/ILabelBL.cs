using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public interface ILabelBL
    {
        Task<List<GetAllLabelResponseModel>> GetAllLabel(string email);
        Task<int> CreateNewLabel(string email, CreateLabelRequestModel labelModel);
        Task<bool> UpdateLabel(UpdateLabelRequestModel labelModel);
        Task<bool> DeleteLabel(int labelId);
    }
}
