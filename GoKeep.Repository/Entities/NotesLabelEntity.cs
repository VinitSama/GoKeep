using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository;

public partial class NotesLabelEntity
{
    public int Id { get; set; }

    public int NoteId { get; set; }

    public int LabelId { get; set; }
    public int UserId { get; set; }
    public bool IsActive { get; set; }

    public virtual LabelEntity Label { get; set; } = null!;

    public virtual NoteEntity Note { get; set; } = null!;
    public virtual UsersKeepEntity User { get; set; } = null!;
}