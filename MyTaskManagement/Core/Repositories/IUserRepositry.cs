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
         ApplicationUser GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanical(string id);
        IEnumerable<ApplicationUser> GetAllUsersWithProjectsAndTasksAndRolesAndFinanical();

        void AddUser(ApplicationUser user,string pass);
        void UpdateRolesToUser(string idUser , string newRole);
        

    }
}