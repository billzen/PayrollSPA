using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.WebApp.Models;
using FluentValidation;

namespace Payroll.WebApp.Infrastructure.Validators
{
    public class LogEntryViewModelValidator : AbstractValidator<LogEntryViewModel>
    {

        public LogEntryViewModelValidator()
        {

            RuleFor(logentry => logentry.Logdate).NotEmpty().WithMessage("Select LogDate");
            RuleFor(logentry => logentry.EntryTime).NotEmpty().WithMessage("Select Entry Time");
            RuleFor(logentry => logentry.DepartTime).NotEmpty().WithMessage("Select Depart Time");
            //******* //We excluded the LogEntryImage property from LogEntryViewModelValidator cause we will be using a specific FileUpload action to upload images.
            //RuleFor(logentry => logentry.LogEntryImage).NotEmpty().WithMessage("Select LogTntry Image");

        }

    }
}