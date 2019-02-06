using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Domain
{
    public class Financialstatus
    {
        public int Id { get; set; }
        public int W_Hours { get; set; }
        public long OTHours { get; set; }
        public int Wh_Rate { get; set; }
        public long OTH_Rate { get; set; }
        public int Total { get; set; }
        public int Bonus { get; set; }
        public string UserId { get; set; }

        public   ApplicationUser User { get; set; }
        public   TTask Task { get; set; }
    }
}