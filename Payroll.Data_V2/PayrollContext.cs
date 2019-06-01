using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Payroll.Data.Configurations;
using Payroll.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;


namespace Payroll.Data
{
    public class PayrollContext : DbContext
    {
        public PayrollContext() : base("PayrollSPA")
        {
   //         Database.SetInitializer<PayrollContext>(null);
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<Error> Error { get; set; }



        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Configurations.Add(new UserConfiguration());
            //modelBuilder.Configurations.Add(new UserRoleConfiguration());
            //modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new EmployeeTypeConfiguration());
            //modelBuilder.Configurations.Add(new FullTimeEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new PartTimeEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new CommissionedEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new LogEntryConfiguration());
            //modelBuilder.Configurations.Add(new TimeCardConfiguration());
        }

    }
    
}
