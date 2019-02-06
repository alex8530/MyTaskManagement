using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Repositories
{
    public interface IUserRepositry : IRepository<ApplicationUser>
    {
    }
}