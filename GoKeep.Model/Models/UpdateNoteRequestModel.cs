using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class UpdateNoteRequestModel
    {
        public string? Title { get; set; } = "";
        public string? Content { get; set; } = "";
    }
}
