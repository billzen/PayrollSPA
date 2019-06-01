using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Entities
{
    public class EmployeeType: IEntityBase
    {
        //[Key]  
        //public int TypeId { get; set; }

        public int ID { get; set; }

        //   [Required(ErrorMessage = "Type Description is a required field")]
        public string TypeDescription { get; set; }
    }
}
