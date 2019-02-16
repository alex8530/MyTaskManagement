using MyTaskManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyTaskManagement.Core;
using System.Threading.Tasks;

namespace MyTaskManagement.Persistence.Repositories
{
    //Note i made  some method in this class , because the way to  get user is different 
    // because i will use manager for the user !!!!!!!!

    //i will not change Get , GetAll because is the same , we dont need manager for them..
    public class UserRepository : Repository<ApplicationUser>, IUserRepositry
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
            

        }

        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }
       

        public void AddUser(ApplicationUser user, string pass)
        {
             var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(CuurentContext));
             var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(CuurentContext));


            var chkUser = userManager.Create(user, pass);
            //Add default User to Role ProjectManager  
            if (chkUser.Succeeded)
            {
                //by default , will be employee , and then the admin can change the role..
                var result1 = userManager.AddToRole(user.Id, ConstantsRolesNames.Employee);
                var result1g = userManager.RemoveFromRole(user.Id, ConstantsRolesNames.Employee);

            }

        }

       

        public IEnumerable<ApplicationUser> GetAllUsersWithProjectsAndTasksAndRoles()
        {
            return CuurentContext.Users.Include(user => user.Tasks)
                .Include(user => user.Projects).Include(user => user.Roles).ToList();
        }

        public  ApplicationUser  GetUserWithProjectsAndTasksAndRoles(string id)
        {
            return CuurentContext.Users.Where(user => user.Id == id).Include(user => user.Tasks)
                .Include(user => user.Projects).Include(user => user.Roles).SingleOrDefault();
        }

        public void UpdateRolesToUser(string idUser, string newRole)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(CuurentContext));
             
            //first delete the preivuos roles
            var roles =   userManager.GetRoles (idUser);
              userManager.RemoveFromRoles (idUser, roles.ToArray());

            //add new role
            userManager.AddToRole(idUser, newRole);
        }
    }
}

