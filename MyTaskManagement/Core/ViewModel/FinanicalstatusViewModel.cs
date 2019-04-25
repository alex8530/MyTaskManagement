using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Core.Domain;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.ViewModel
{
    public class FinanicalstatusViewModel
    {

        public ApplicationUser User{ get; set; }

 
        public IEnumerable<Financialstatus> Financialstatus { get; set; }


    }
}