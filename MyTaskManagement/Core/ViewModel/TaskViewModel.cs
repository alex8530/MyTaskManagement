using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.ViewModel
{
    public class CreateTaskViewModel
    {

        public TTask  Task { get; set; }
        
        //we pass here list DayOfWeek user , to show all users in DBNull and let user choose one from select
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Project>Projects{ get; set; }
     


    }
    public class EditTaskViewModel
    {

        public TTask  Task { get; set; }
        
        //we pass here list DayOfWeek user , to show all users in DBNull and let user choose one from select
        public IEnumerable<ApplicationUser> Users { get; set; }
       


    }


}