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
            Database.SetInitializer<PayrollContext>(null);
        }

        public IDbSet<Employee> Employee { get; set; }
        public IDbSet<EmployeeType> EmployeeType { get; set; }
        public IDbSet<Error> Error { get; set; }
        public IDbSet<TimeCard> TimeCard { get; set; }
        public IDbSet<LogEntry> LogEntry { get; set; }
        public IDbSet<FullTimeEmployee> FullTimeEmployee { get; set; }
        public IDbSet<PartTimeEmployee> PartTimeEmployee { get; set; }
        public IDbSet<CommissionedEmployee> CommissionedEmployee { get; set; }
        public IDbSet<EmployeeOrder> EmployeeOrder { get; set; }
        public IDbSet<Order> Order { get; set; }
        public IDbSet<OrderLine> OrderLine { get; set; }
        public IDbSet<Product> Product { get; set; }
        public IDbSet<ProductImage> ProductImage { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());

            //*******************Not neccassery Probably for CODE FIRST ????
            //modelBuilder.Configurations.Add(new EmployeeConfiguration());
            //modelBuilder.Configurations.Add(new EmployeeTypeConfiguration());
            //modelBuilder.Configurations.Add(new TimeCardConfiguration());
            //modelBuilder.Configurations.Add(new FullTimeEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new PartTimeEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new CommissionedEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new LogEntryConfiguration());

        }

    }
    
}
