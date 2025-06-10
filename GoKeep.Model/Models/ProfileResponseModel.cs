using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class ProfileResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string? Lastname { get; set; }
        public List<string> Roles { get; set; }
    }
}
