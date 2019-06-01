using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.WebApp.Models;
using FluentValidation;

namespace Payroll.WebApp.Infrastructure.Validators
{
    public class FullTimeEmployeeViewModelValidator :AbstractValidator<FullTimeEmployeeViewModel>
    {
        public FullTimeEmployeeViewModelValidator()
        {
            RuleFor(fulltimeemloyee => fulltimeemloyee.EmployeeMonthlySalary).NotEmpty().WithMessage("Select an Employee Monthly Salary");
        }
    }
}