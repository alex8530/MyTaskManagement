﻿using MyTaskManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Models;
using System.Linq.Expressions;
using System.Data.Entity;

namespace MyTaskManagement.Persistence.Repositories
{

    public class ProjectRepository : Repository<Project>, IProjectRepositry
    {
        public ProjectRepository(ApplicationDbContext context)
            : base(context)
        {

        }





        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }

    

        public IEnumerable<Project> GetAllProjectsWithClientAndUsersAndTasksWithFiles()
        {
            return CuurentContext.Projects.Include(project => project.Client).Include(project => project.Users).Include(project => project.ProjectFiles).Include(project => project.Tasks).ToList();
        }

        public Project GetProjectsWithClientAndUsersAndTasksWithFiles(string id)
        {
            return CuurentContext.Projects.Where(p => p.Id == id).Include(project => project.Client).Include(project => project.ProjectFiles).Include(project => project.Users).Include(project => project.Tasks).SingleOrDefault() ;
        }

       

    
    }
}

