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
         ApplicationUser GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFilesWithPayments(string id);
        IEnumerable<ApplicationUser> GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFilesWithPayments();

        void AddUser(ApplicationUser user,string pass);
        void UpdateRolesToUser(string idUser , string newRole);
        

    }
}