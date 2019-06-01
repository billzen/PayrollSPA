using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.WebApp.Models;
using FluentValidation;

namespace Payroll.WebApp.Infrastructure.Validators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {

        public ProductViewModelValidator()
        {
            RuleFor(product => product.ProductDescription).NotEmpty().WithMessage("Select Product Description");
            RuleFor(product => product.Price).NotEmpty().WithMessage("Select product Price");
            RuleFor(product => product.Discount).NotEmpty().WithMessage("Select product Discount");
        }   
            
    }
}