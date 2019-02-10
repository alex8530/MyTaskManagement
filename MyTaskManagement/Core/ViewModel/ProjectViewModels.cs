using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.ViewModel
{
    public class IndexViewModels
    {
       
        public Project Project   { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Client> Clients { get; set; }

 

    }

    public class EditViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}