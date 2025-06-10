using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class RefreshTokenModel
    {
        public string Email { get; set; }
        public string OldToken { get; set; }
    }
}
