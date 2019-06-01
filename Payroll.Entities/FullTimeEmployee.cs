using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class FullTimeEmployee : IEntityBase  // Employee
    {


        public int ID { get; set; }      //********** new

        public int EmpId { get; set; }

        //public virtual Employee Employee { get; set; }

        ////public override string FirstName { get; set; }
        //public override string FirstName
        //{
        //     get { return base.FirstName; }
        //}

        ////public override string LastName { get; set; }
        //public override string LastName
        //{
        //    get { return base.LastName; }
        //}

        public decimal EmployeeMonthlySalary { get; set; }


    }
}
