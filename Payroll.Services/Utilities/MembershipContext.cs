using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Entities;
using System.Security.Principal;

namespace Payroll.Services.Utilities
{
   public class MembershipContext
    {
       public IPrincipal Principal { get; set; }

       public User User { get; set; }

       public bool isValid() 
       {
           return Principal != null;
       }
    }
}
