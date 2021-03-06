﻿using MyTaskManagement.Core.Repositories;
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
    
    public class FinancialRepositry : Repository<Financialstatus>, IFinancialRepositry
    {
        public FinancialRepositry(ApplicationDbContext context)
            : base(context)
        {

        }


        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<Financialstatus> GetAllFinancialstatusWithUser()
        {
            return CuurentContext.Financialstatuses.Include(f => f.User).ToList();
        }
    }
}

