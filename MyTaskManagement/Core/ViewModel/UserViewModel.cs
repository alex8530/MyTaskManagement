using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.ViewModel
{
    public class ListUserViewModel
    {



        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
         

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}