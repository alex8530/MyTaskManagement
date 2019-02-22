using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Domain
{
    public class Financialstatus
    {
         
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        public int W_Hours { get; set; }

        public int OTHours { get; set; }
        public int Wh_Rate { get; set; }
        public int OTH_Rate { get; set; }
        public int Total { get; set; }
        public int Bonus { get; set; }
        public string pro__id { get; set; }

        public string task__id { get; set; }

        [ForeignKey("User")]
        public string user__id { get; set; }

         
        
        public ApplicationUser User { get; set; }
         
        //[Required]//this is because Financialstatus depend on Task
        //public TTask Task { get; set; }
    }
}