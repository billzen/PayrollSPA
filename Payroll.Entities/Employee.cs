using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Entities
{
    public class Employee: IEntityBase
    {

        //public Employee()
        //{
        //    FullTimeEmployee = new FullTimeEmployee();
        //}

        public int ID { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Address { get; set; }


        public string City { get; set; }


        public string PostCode { get; set; }


        public int Phone { get; set; }


        public string Email { get; set; }


        public int DepartmentNo { get; set; }


        public int TypeId { get; set; }

        public int TimeCardId { get; set; }

        // public virtual ICollection<FullTimeEmployee> FullTimeEmployees { get; set; }
        // public virtual FullTimeEmployee FullTimeEmployee { get; set; }
    }
}
