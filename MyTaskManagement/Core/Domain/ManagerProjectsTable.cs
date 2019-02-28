using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Domain
{
    public class ManagerProjectsTable
    {
        //project must be uniqe so i will put it as key
        [Key]
        public string  ProjectId { get; set; }
        public string ManagerId { get; set; }

    }
}