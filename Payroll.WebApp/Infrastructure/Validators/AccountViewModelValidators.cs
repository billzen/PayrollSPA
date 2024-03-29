﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Payroll.WebApp.Models;

namespace Payroll.WebApp.Infrastructure.Validators
{
    class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {

        public RegistrationViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid password");
        }
    }


    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid password");
        }
    }
}

