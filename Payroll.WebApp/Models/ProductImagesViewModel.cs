using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Payroll.WebApp.Infrastructure.Validators;

namespace Payroll.WebApp.Models
{
    public class ProductImagesViewModel //: IValidatableObject
    {
        public int ID { get; set; }

        public int ProductId { get; set; }

        public Nullable<int> Filesize { get; set; }

        public string LogFilename { get; set; }

        public byte[] Filebytes { get; set; }

        //**********************  VALIDATE Image file extension(gif-jpj) - Image size > 20 Mb etc

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var validator = new ProductViewModelValidator();
        //    var result = validator.Validate(this);
        //    return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        //}

    }
}