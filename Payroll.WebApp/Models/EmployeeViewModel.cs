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
    /// <summary>
    /// *********************   FILL IF IS necassery
    /// </summary>
    public class EmployeeViewModel : IValidatableObject
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public int DepartmentNo { get; set; }

        //******************public string EmployeeType { get; set; }

        public int TypeId { get; set; }

        public int TimeCardId { get; set; }




        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EmployeeViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }


    }
}
