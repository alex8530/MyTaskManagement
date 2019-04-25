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

    public class PaymentRepository : Repository<Payment>, IPaymentRepositry
    {
        public PaymentRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public ApplicationDbContext CuurentContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return CuurentContext.Payments.Include(u => u.ApplicaionUser).ToList();
        }

        public Payment GetPayment(string userid)
        {
            return CuurentContext.Payments.Include(u => u.ApplicaionUser).Where(p=>p.ApplicationUserId ==userid).SingleOrDefault();

        }
    }
}

