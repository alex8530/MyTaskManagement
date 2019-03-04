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

        public string Description { get; set; }



        [Required]
        public long EstimatedTime { get; set; }

        [Required]
        public long EffortHours { get; set; }

       [Required]
        public long RemainingHours {
           get
           {
               return this.EstimatedTime - this.EffortHours;

           }

       }


         
        public string Ticket { get; set; }

        public string Notes { get; set; }
        public string Owner { get; set; }


        //[Required]
        public ApplicationUser ApplicationUser { get; set; }

        //[Required]
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }


       
        public string ProjectId { get; set; }


       

         
    }
}   