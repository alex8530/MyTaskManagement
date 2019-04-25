using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.Domain
{
    public class Payment
    {
        public int Id { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public string ApplicationUserId { get; set; }


        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicaionUser { get; set; }
         
        public string Note { get; set; }

        [DisplayName("Amount Of Money $")]
        public int AmountOfMoney { get; set; }
    }
}