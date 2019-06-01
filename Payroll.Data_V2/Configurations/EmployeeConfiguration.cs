using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payroll.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Payroll.Data.Configurations
{
    public class EmployeeConfiguration : EntityBaseConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee");

            Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            Property(e => e.LastName).IsRequired().HasMaxLength(50);
            Property(e => e.City).IsRequired().HasMaxLength(50);
            Property(e => e.PostCode).IsRequired().HasMaxLength(50);
            Property(e => e.Phone).IsRequired();
            Property(e => e.Email).IsRequired().HasMaxLength(50);
            Property(e => e.DepartmentNo).IsRequired();
            Property(e => e.TypeId).IsRequired();
            Property(e => e.TimeCardId).IsRequired();
        }
    }
}