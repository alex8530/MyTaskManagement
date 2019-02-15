using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MyTaskManagement.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTaskManagement.Startup))]
namespace MyTaskManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createMyRolesandUsers();
        }

        private void createMyRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
          
            if (!RoleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new IdentityRole();
                role.Name = "Admin";
                RoleManager.Create(role);

             
            }
            // creating Creating Manager role   
            if (!RoleManager.RoleExists("ProjectManager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                RoleManager.Create(role);

            }

            // creating Creating Employee role   
            if (!RoleManager.RoleExists("Employee"))
            {
                var role = new  IdentityRole();
                role.Name = "Employee";
                RoleManager.Create(role);

            }

            //Here we create a Admin super user who will maintain the website                 

            var user = new ApplicationUser();
            user.UserName = "superadmin";
            user.Email = "superadmin@superadmin.com";

            string userPWD = "123123";

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin  
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Admin");

            }

        }
    }
}
