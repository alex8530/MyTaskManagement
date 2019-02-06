using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Core;
using MyTaskManagement.Models;

namespace MyTaskManagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            //Courses = new CourseRepository(_context);
            //Authors = new AuthorRepository(_context);
        }

        //public ICourseRepository Courses { get; private set; }
        //public IAuthorRepository Authors { get; private set; }

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