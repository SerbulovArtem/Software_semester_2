using ActionManager.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_MVC.Models
{
    public class TblActionModel
    {
        [Display(Name = "Action ID")]
        public int ActionId { get; set; }

        [Display(Name = "Product ID")]
        [ValidateNever]
        public int ProductId { get; set; }

        [Display(Name = "Discount Percentage")]
        [ValidateNever]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Type Action ID")]
        [ValidateNever]
        public int TypeActionId { get; set; }

        [Display(Name = "Insert Time")]
        [ValidateNever]
        public DateTime InsertTime { get; set; }

        [Display(Name = "Update Time")]
        [ValidateNever]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "ProductId")]
        [ValidateNever]
        public virtual TblProductModel Product { get; set; }

        [Display(Name = "TypeActionId")]
        [ValidateNever]
        public virtual TblTypeActionModel TypeAction { get; set; }
    }
}