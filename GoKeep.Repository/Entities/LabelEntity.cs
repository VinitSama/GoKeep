using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository;

public partial class LabelEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<NotesLabelEntity> NotesLabels { get; set; } = new List<NotesLabelEntity>();

    public virtual UsersKeepEntity User { get; set; } = null!;
}
