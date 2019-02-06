﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Models
{
    public class Project
    {

        [Key]
        [Required]
        public string  Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public  DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public  DateTime DeadTime { get; set; }


        public string Description { get; set; }

        [Required]
        public StatusEnum Status { get; set; }


        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<TTask> Tasks { get; set; }

        [Required]
        public  Client Client { get; set; }
       
    }
}