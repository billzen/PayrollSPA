using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Payroll.WebApp.Models
{
    public class CommissionedEmployeeDetailViewModel
    {
        public int ID { get; set; }

        public int EmpId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool WeeeklyPaid { get; set; }

        public decimal MonthlyWorkingHours { get; set; }

        public decimal EmployeeMonthlySalary { get; set; }
    }
}