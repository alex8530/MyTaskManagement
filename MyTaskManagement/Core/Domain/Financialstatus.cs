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


        public double EstimatedHours { get; set; }
        public double EffortHours { get; set; }
        
    
        public double Payment { get; set; }

        public double Remain { get; set; }
        
        public string Pro__id { get; set; }

        public string Task__id { get; set; }

        [ForeignKey("User")]
        public string User__id { get; set; }

         
        
        public ApplicationUser User { get; set; }
         
        //[Required]//this is because Financialstatus depend on Task
        //public TTask Task { get; set; }
    }
}