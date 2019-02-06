using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Models
{
    public class Project
    {

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public  DateTime StartTime { get; set; }
        public  DateTime DeadTime { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public  ICollection<ApplicationUser> Users { get; set; }
        public  ICollection<TTask> Tasks { get; set; }
        public  Client Client { get; set; }
    }
}