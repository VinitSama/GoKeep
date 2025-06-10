using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class RefreshCreateRequestModel(int userId, string token, int days)
    {
        public int UserId { get; set; } = userId;
        public string Token { get; set; } = token;
        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddDays(days);
    }
}
