using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.WebApp.Models;
using FluentValidation;

namespace Payroll.WebApp.Infrastructure.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(order => order.OrderDate).NotEmpty().WithMessage("Select Order date");
            RuleFor(order => order.Orderdescription).NotEmpty().Length(1, 50).WithMessage("Select Order description");
            RuleFor(order => order.OrderAmount).NotEmpty().WithMessage("Select Order Amount");
        }
    }
}