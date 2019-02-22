using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyTaskManagement.Core.Domain;

namespace MyTaskManagement.Core.ViewModel
{
    public class ListUserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public List<string> RolesNames { get; set; }

    }

    public class EditUserViewModel
    {
        public  ApplicationUser  User { get; set; }
        public List<Financialstatus>   Financialstatuses { get; set; }

    }
}