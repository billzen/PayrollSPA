﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.WebApp.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace Payroll.WebApp.Models
{
    public class RegistrationViewModel: IValidatableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new RegistrationViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }

    }
}
