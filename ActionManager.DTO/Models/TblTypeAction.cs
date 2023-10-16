using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ActionManager.DTO;

[Table("tblTypeActions")]
public partial class TblTypeAction
{
    [Key]
    [Column("type_action_id")]
    public int TypeActionId { get; set; }

    [Column("type_action_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string TypeActionName { get; set; } = null!;

    [InverseProperty("TypeAction")]
    public virtual ICollection<TblAction> TblActions { get; set; } = new List<TblAction>();
}
