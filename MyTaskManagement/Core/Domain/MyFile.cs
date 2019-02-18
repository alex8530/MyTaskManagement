using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.Domain
{
    public class MyFile
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public MyFileType  MyFileType { get; set; }

        [ForeignKey("UserFile")]
        public string UserFileId { get; set; }
        public ApplicationUser UserFile { get; set; }

    }

}