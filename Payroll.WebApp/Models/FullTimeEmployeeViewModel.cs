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
    public class FullTimeEmployeeViewModel : IValidatableObject
    {

        public int ID { get; set; }

        public int EmpId { get; set; }

        public decimal EmployeeMonthlySalary { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new FullTimeEmployeeViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }

    }
}