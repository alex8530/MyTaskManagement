﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Repositories
{
    public interface IProjectRepositry : IRepository<Project>
    {
        //if i need method for project<<<<<<<<< put it here
         
        IEnumerable<Project> GetAllProjectsWithClientAndUsersAndTasksWithFiles();
       Project GetProjectsWithClientAndUsersAndTasksWithFiles(string id);
       
    }
}