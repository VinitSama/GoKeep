using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository;

public partial class UsersKeepEntity
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<LabelEntity> Labels { get; set; } = new List<LabelEntity>();

    public virtual ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    public virtual ICollection<NotesLabelEntity> NotesLabels { get; set; } = new List<NotesLabelEntity>();
}
