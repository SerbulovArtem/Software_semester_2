using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ActionManager.DTO;

[Table("tblActions")]
public partial class TblAction
{
    [Key]
    [Column("action_id")]
    public int ActionId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("discount_percentage", TypeName = "decimal(5, 2)")]
    public decimal DiscountPercentage { get; set; }

    [Column("type_action_id")]
    public int TypeActionId { get; set; }

    [Column("insert_time")]
    public DateTime InsertTime { get; set; }

    [Column("update_time")]
    public DateTime UpdateTime { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TblActions")]
    public virtual TblProduct Product { get; set; } = null!;

    [ForeignKey("TypeActionId")]
    [InverseProperty("TblActions")]
    public virtual TblTypeAction TypeAction { get; set; } = null!;
}
