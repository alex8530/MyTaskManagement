using MyTaskManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;
using System.Linq.Expressions;
using System.Data.Entity;

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
    }
}

