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
        public string Title { get; set; }

        [Required]
        public PriorityEnum Priority { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        [Required]
        public TypeTaskEnum TypeTask { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; } 

        public string Description { get; set; }

        public bool IsApproveByManager { get; set; }

        [DisplayName("Estimated H")]
        [Required]
        public long EstimatedTime { get; set; }

        [DisplayName("Effort H")]
        [Required]
        public long EffortHours { get; set; }

       [Required]
        [DisplayName("Remaining H")]

        public long RemainingHours {
           get
           {
               return this.EstimatedTime - this.EffortHours;

           }

       }

        [Required]
        [DisplayName("Approved H")]
        public int ApproveHourByManager { get; set; }

        public string Ticket { get; set; }

        public string Notes { get; set; }
        public string Creator { get; set; }
        public string Owner { get; set; }



        public string ReviewerId { get; set; }
        [DisplayName("Reviewer")]
        public string ReviewerName{ get; set; }
         

        //[Required]
        public ApplicationUser ApplicationUser { get; set; }

        //[Required]
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public string ProjectId { get; set; }


        //this is not for database
        [NotMapped]
        public bool IsUpdate { get; set; }

        public bool? ComeFromClone { get; set; }

        public string FromUser { get; set; }
        public string fromtaskid { get; set; }






    }
}   