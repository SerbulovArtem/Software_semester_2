using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ActionManager.DTO;

[Table("tblProducts")]
public partial class TblProduct
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("product_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string ProductName { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<TblAction> TblActions { get; set; } = new List<TblAction>();
}
