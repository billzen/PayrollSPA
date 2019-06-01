using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class CommissionedEmployee: IEntityBase
    {
        public int ID { get; set; }

        public int EmpId { get; set; }

        public bool WeeeklyPaid { get; set; }

         public decimal MonthlyWorkingHours { get; set; }

        public decimal EmployeeMonthlySalary { get; set; }
    }
}
