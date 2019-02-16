﻿using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.ViewModel
{
    public class ListUserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public List<string> RolesNames { get; set; }

    }
}