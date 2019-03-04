using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.Domain
{
    public class ProjectManagerTable
    {
         //buz there must be one manager for each project ,,project must be uniqe
        [Key]
        public string ProjectID { get; set; }


        public string ManagerID { get; set; }

    }
}