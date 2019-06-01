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
    {
        public static bool EmployeeExists(this IEntityBaseRepository<Employee> emplloyeesRepository, string FirstName, string LastName) 
        {
            bool _employeeExist = false;

            _employeeExist = emplloyeesRepository.GetAll()
                .Any(e => e.FirstName == FirstName && e.LastName == LastName);  

            return _employeeExist;
        }
   
    }
}
