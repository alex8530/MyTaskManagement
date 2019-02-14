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

    public class TTaskRepository : Repository<TTask>, ITTaskRepositry
    {
        public TTaskRepository(ApplicationDbContext context)
            : base(context)
        {

        }





        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<TTask> GetAllTasksWithUserAndUserAndProject()
        {
            return CuurentContext.Tasks.Include(task => task.ApplicationUser).Include(task => task.Project).ToList();
        }

        public TTask GetTasksWithUserAndUserAndProject(int id)
        {
            return CuurentContext.Tasks.Where(task => task.Id == id).Include(task => task.Project)
                .Include(task => task.ApplicationUser).SingleOrDefault();
        }
    }
}

