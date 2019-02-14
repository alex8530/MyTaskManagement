using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyTaskManagement.Core.Domain;

namespace MyTaskManagement.Models
{
    public class TTask
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public PriorityEnum Priority { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeadTime { get; set; }


        public string Description { get; set; }

        [Required]
        public int WorkingHours { get; set; }


        [Required]
        public int OverTime { get; set; }


        //[Required]
        public ApplicationUser ApplicationUser { get; set; }

        //[Required]
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }


       
        public string ProjectId { get; set; }


        //public Financialstatus  Financialstatus { get; set; }

         
    }
}   