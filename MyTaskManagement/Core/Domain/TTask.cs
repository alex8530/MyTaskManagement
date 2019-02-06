using System;
using MyTaskManagement.Core.Domain;

namespace MyTaskManagement.Models
{
    public class TTask
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime DeadTime { get; set; }
        public string Description { get; set; }
        public int WorkingHours { get; set; }
        public int OverTime { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Project Project { get; set; }


        public Financialstatus    Financialstatus { get; set; }
            
    }
}   