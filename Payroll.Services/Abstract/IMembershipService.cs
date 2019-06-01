using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Entities;
using Payroll.Services.Utilities;


namespace Payroll.Services.Abstract
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string usrname, string password);

        User CreateUser(string username, string email, string password, int[] roles);

        User GetUser(int userId);

        List<Role> GetUserRoles(string username);
    }
}
