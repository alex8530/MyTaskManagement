using MyTaskManagement.Core.Domain;
using MyTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTaskManagement.Core.ViewModel
{
    public class PaymentViewModel
    {
        public ApplicationUser User { get; set; }


        public IEnumerable<Payment>  Payments{ get; set; }

    }
}