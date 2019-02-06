using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Core.Repositories;

namespace MyTaskManagement.Core
{
    public interface IUnitOfWork : IDisposable
    {
        //ICourseRepository Courses { get; }
        IProjectRepositry  ProjectRepositry { get; }
        int Complete();
    }
}