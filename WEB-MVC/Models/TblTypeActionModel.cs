using ActionManager.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_MVC.Models
{
    public class TblTypeActionModel
    {
        [Display(Name = "Type Action ID")]
        public int TypeActionId { get; set; }

        [Display(Name = "Type Action Name")]
        [ValidateNever]
        public string TypeActionName { get; set; }

        [Display(Name = "Type Actions")]
        [ValidateNever]
        public ICollection<TblActionModel> TblActions { get; set; }
    }
}
