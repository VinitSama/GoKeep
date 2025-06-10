using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository;

public partial class NoteEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public bool IsPinned { get; set; }

    public bool IsArchived { get; set; }

    public bool IsTrashed { get; set; }
    public bool DeleteForever { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<NotesLabelEntity> NotesLabels { get; set; } = new List<NotesLabelEntity>();

    public virtual UsersKeepEntity User { get; set; } = null!;
}
