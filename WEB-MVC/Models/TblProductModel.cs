using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB_MVC.Models;

namespace WEB_MVC.Models
{
    public class TblProductModel
    {
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [ValidateNever]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Actions")]
        [ValidateNever]
        public ICollection<TblActionModel> TblActions { get; set; }
    }
}
