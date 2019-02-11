using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTaskManagement.Models
{
    public class Client
    {
         
        public int Id { get; set; }

        [DisplayName("Client Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AdditionInformation { get; set; }

         public  ICollection<Project> Projects { get; set; }
    }
}