using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Repositories
{
    public interface IProjectRepositry : IRepository<Project>
    {
        //if i need method for project<<<<<<<<< put it here

        IEnumerable<Project> GetAllProjectsWithClientAndUsers();
        IEnumerable<Project> GetAllProjectsWithClientAndUsersAndTasks();
       Project GetProjectsWithClientAndUsersAndTasks(string id);
       Project  GetProjectsWithClientAndUsers(string id);
    }
}