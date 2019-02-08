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
    
    public class ClientRepository : Repository<Client>, IClientRepositry
    {
        public ClientRepository(ApplicationDbContext context)
            : base(context)
        {

        }

         


        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}

