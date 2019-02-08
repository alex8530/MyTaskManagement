using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskManagement.Core.Domain;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Repositories
{
    public interface IClientRepositry : IRepository<Client>
    {
    }
}