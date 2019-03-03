using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.Domain
{
    public class MyProjectFile
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public MyFileType  MyFileType { get; set; }

        [ForeignKey("Project")]
        public string ProjectId { get; set; }
        public Project Project { get; set; }



    }

}