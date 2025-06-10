using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class TokenResponseModel
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
