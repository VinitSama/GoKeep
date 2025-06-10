using GoKeep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public interface IUserRL
    {
        Task<bool> RegisterUser(RegisterRequestModel userModel, string passwordHash);
        Task<LoginResponseModel?> LoginUser(LoginRequestModel userModel);
        Task<string> RefreshToken(RefreshTokenModel refreshModel);
    }
}
