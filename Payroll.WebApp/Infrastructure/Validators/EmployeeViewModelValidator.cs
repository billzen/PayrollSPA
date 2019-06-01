using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.WebApp.Models;
using FluentValidation;


namespace Payroll.WebApp.Infrastructure.Validators
{
    public class EmployeeViewModelValidator : AbstractValidator<EmployeeViewModel>
    {
        public EmployeeViewModelValidator()
        {

            RuleFor(employee => employee.FirstName).NotEmpty().Length(1, 50).WithMessage("Select a First Name");

            RuleFor(employee => employee.LastName).NotEmpty().Length(1, 50).WithMessage("Select a Last Name");

            RuleFor(employee => employee.Address).NotEmpty().Length(1, 50).WithMessage("Select an Address");

            RuleFor(employee => employee.City).NotEmpty().Length(1, 50).WithMessage("Select a City");

            RuleFor(employee => employee.PostCode).NotEmpty().Length(1, 50).WithMessage("Select a Post Code");

            RuleFor(employee => employee.Phone).NotEmpty().WithMessage("Select a Phone Number");

            RuleFor(employee => employee.Email).NotEmpty().EmailAddress().WithMessage("Select a Email");

            RuleFor(employee => employee.DepartmentNo).InclusiveBetween(1, 10).WithMessage("Select a Department between 1 to 10 ");

            RuleFor(employee => employee.TypeId).GreaterThan(0).WithMessage("Select a Type");
        }

    }
}
