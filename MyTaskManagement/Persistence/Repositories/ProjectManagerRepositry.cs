using MyTaskManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using MyTaskManagement.Core.Domain;

namespace MyTaskManagement.Persistence.Repositories
{
    
    public class ProjectManagerRepositry : Repository<ProjectManagerTable>, IProjectMangerRepositry
    {
        public ProjectManagerRepositry(ApplicationDbContext context)
            : base(context)
        {

        }


        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }

        
    }
}

