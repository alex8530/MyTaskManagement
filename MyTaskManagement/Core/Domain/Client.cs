using System.Collections.Generic;

namespace MyTaskManagement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AdditionInformation { get; set; }

         public virtual ICollection<Project> Projects { get; set; }
    }
}