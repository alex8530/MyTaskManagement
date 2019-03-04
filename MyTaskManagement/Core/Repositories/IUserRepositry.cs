using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Repositories
{
    public interface IUserRepositry : IRepository<ApplicationUser>
    {
         ApplicationUser GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(string id);
        IEnumerable<ApplicationUser> GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFiles();

        void AddUser(ApplicationUser user,string pass);
        void UpdateRolesToUser(string idUser , string newRole);
        

    }
}