using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Data.Repositories;
using Payroll.Entities;

namespace Payroll.Data.Extensions
{
    public static class EmployeeExtensions
    {/// <summary>
    ///*********** Bussines Rule: not insert Employess with same FistName & LastName 
    /// </summary>
    /// <param name="employeesRepository"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <returns></returns>
        public static bool EmployeeExists(this IEntityBaseRepository<Employee> employeesRepository, string FirstName, string LastName) 
        {
            bool _employeeExist = false;

            _employeeExist = employeesRepository.GetAll()
                .Any(e => e.FirstName == FirstName && e.LastName == LastName);  

            return _employeeExist;
        }
   
    }
}
