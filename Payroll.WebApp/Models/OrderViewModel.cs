using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Payroll.WebApp.Infrastructure.Validators;

namespace Payroll.WebApp.Models
{
    public class OrderViewModel : IValidatableObject
    {
        public int ID { get; set; }

        public Nullable<System.DateTime> OrderDate { get; set; }

        [StringLength(50, ErrorMessage = "Order description max Length is 50")]
        public string Orderdescription { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid OrderAmount Value; Maximum Two Decimal Points.")]
        public Nullable<decimal> OrderAmount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new OrderViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}