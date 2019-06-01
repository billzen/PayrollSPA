using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.WebApp.Models;
using FluentValidation;
using System.Web;

namespace Payroll.WebApp.Infrastructure.Validators
{
    public class CommissionedEmployeeViewModelValidator : AbstractValidator<CommissionedEmployeeViewModel>
    {

        public CommissionedEmployeeViewModelValidator()
        {

            RuleFor(commisionedemployee => commisionedemployee.MonthlyWorkingHours).NotEmpty().WithMessage("Select MonthlyWorkingHours");

            RuleFor(commisionedemployee => commisionedemployee.EmployeeMonthlySalary).NotEmpty().WithMessage("Select EmployeeMonthlySalary");
        }
    }
}

