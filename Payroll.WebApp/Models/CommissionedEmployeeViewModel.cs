using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Payroll.WebApp.Infrastructure.Validators;

namespace Payroll.WebApp.Models
{
    public class CommissionedEmployeeViewModel : IValidatableObject
    {
        public int ID { get; set; }

        public int EmpId { get; set; }

        public bool WeeeklyPaid { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Monthly Working sHours Value; Maximum Two Decimal Points.")]
        public decimal MonthlyWorkingHours { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Employee Monthly Salary Value; Maximum Two Decimal Points.")]
        public decimal EmployeeMonthlySalary { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new CommissionedEmployeeViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }

    }
}