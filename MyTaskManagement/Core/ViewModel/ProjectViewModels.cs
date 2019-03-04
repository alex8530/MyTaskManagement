using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.ViewModel
{
    public class CreateProjectViewModels
    {

        public Project Project { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public ApplicationUser  Manager  { get; set; }



    }
    public class IndexProjectViewModel
    {
        public List<Project> Projects { get; set; }
        public List<ApplicationUser> Managers { get; set; }
     }

}