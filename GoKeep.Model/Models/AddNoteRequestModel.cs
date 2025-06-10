using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class AddNoteRequestModel
    {
        public string? Title { get; set; } = "";
        public string? Content { get; set; } = "";
        public bool IsPinned { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public bool IsTrashed { get; set; } = false;
    }
}
