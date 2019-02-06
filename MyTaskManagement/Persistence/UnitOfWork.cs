using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Core;
using MyTaskManagement.Core.Repositories;
using MyTaskManagement.Models;
using MyTaskManagement.Persistence.Repositories;

namespace MyTaskManagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
          
            ProjectRepositry= new ProjectRepository(_context);
        }

        public IProjectRepositry ProjectRepositry { get; private set; }
 

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}