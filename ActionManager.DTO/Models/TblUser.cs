using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ActionManager.DTO;

[Table("tblUsers")]
public partial class TblUser
{
    [Key]
    [Column("username")]
    [StringLength(255)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("salt")]
    [StringLength(255)]
    [Unicode(false)]
    public string Salt { get; set; } = null!;
}
