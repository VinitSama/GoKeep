using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Business
{
    public interface IUserBL
    {
        Task<bool> RegisterUser(RegisterRequestModel userModel);
        Task<TokenResponseModel> LoginUser(LoginRequestModel userModel);
        Task<TokenResponseModel> RefreshToken(RefreshTokenModel refreshModel);
    }
}
