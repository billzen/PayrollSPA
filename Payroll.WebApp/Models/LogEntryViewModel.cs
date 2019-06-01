using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Payroll.WebApp.Infrastructure.Validators;

namespace Payroll.WebApp.Models
{
    //*** We excluded the LogEntryImage property from LogEntryViewModel binding cause we will be using a specific FileUpload action to upload images.
    [Bind(Exclude = "LogEntryImage")]
    public class LogEntryViewModel
    {
        public int ID { get; set; }

        public int TimeCardId { get; set; }

        public DateTime Logdate { get; set; }

        public TimeSpan EntryTime { get; set; }

        public TimeSpan DepartTime { get; set; }

        public TimeSpan WorkerdHours { get; set; }

       public string LogEntryImage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new LogEntryViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}