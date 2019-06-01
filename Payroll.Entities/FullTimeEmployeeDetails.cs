using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace Payroll.Entities
{

    public class FullTimeEmployeeDetails : IEntityBase  
    {

        public int ID { get; set; }      

        public int EmpId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal EmployeeMonthlySalary { get; set; }


    }
}
