﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Core.Repositories;

namespace MyTaskManagement.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITTaskRepositry  TTaskRepositry { get; }
        IProjectRepositry  ProjectRepositry { get; }
        IUserRepositry   UserRepositry { get; }
        int Complete();
    }
}