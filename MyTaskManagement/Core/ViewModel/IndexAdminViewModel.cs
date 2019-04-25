using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.ViewModel
{

    public class IndexAdminViewModel
    {
        public List<Project> Projects { get; set; }
        public List<TTask> Tasks { get; set; }
        public List<ApplicationUser> Users { get; set; }

    }
}