using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.ViewModel
{
    public class TaskViewModel  
    {

        public TTask  Task { get; set; }
        
        //we pass here list DayOfWeek user , to show all users in DBNull and let user choose one from select
        public IEnumerable<ApplicationUser> Users { get; set; }
     


    }
}