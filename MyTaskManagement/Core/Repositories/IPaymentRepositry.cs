using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyTaskManagement.Core.Domain;
using MyTaskManagement.Models;

namespace MyTaskManagement.Core.Repositories
{
    public interface IPaymentRepositry : IRepository<Payment>
    {
         Payment GetPayment(string userid);
        IEnumerable<Payment> GetAllPayments( );

        IEnumerable<Payment> GetAllPaymentsForEmployee(string id);



    }
}