using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.WebApp.Models;
using FluentValidation;

namespace Payroll.WebApp.Infrastructure.Validators
{
    public class PartTimeEmployeeViewModelValidator : AbstractValidator<PartTimeEmployeeViewModel>
    {

        public PartTimeEmployeeViewModelValidator()
        {
            RuleFor(parttimeemloyee => parttimeemloyee.PartTimeEmployeeRate).NotEmpty().WithMessage("Select an Employee Rate");
        }
    }
}