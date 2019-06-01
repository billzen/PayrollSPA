using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Payroll.Entities;


namespace Payroll.Data.Configurations
{
    public class EmployeeTypeConfiguration : EntityBaseConfiguration<EmployeeType>
    {
        public EmployeeTypeConfiguration()
        {
            ToTable("EmployeeType");
            Property(g => g.TypeDescription).IsRequired().HasMaxLength(50);
        }
    }
}
