using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Data.Extensions
{
    /// <summary>
    /// Extended Class for returning FullTimeEmployee with First name & Last Name. Using LING to objects
    /// </summary>
    public class FullTimeDetailsExtensions
    {

        private readonly Payroll.Data.PayrollContext pr = new Data.PayrollContext();


        public int ID { get; set; }

        public int EmpId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal EmployeeMonthlySalary { get; set; }



 

        public IEnumerable<FullTimeDetailsExtensions> GetAllFullTimeEmployeesWithTypeDescription()
        {
            var FullDesc =
                                                (from aa in pr.Employee
                                                 join bb in pr.FullTimeEmployee
                                                 on aa.ID equals bb.EmpId
                                                 where aa.TypeId == 1
                                                 select new
                                                 {
                                                     ID = bb.ID,
                                                     EmpId = aa.ID,
                                                     FirstName = aa.FirstName,
                                                     LastName = aa.LastName,
                                                     EmployeeMonthlySalary = bb.EmployeeMonthlySalary
                                                 }).AsEnumerable().Select(x => new FullTimeDetailsExtensions
                                                 {
                                                     ID = x.ID,
                                                     EmpId = x.EmpId,
                                                     FirstName = x.FirstName,
                                                     LastName = x.LastName,
                                                     EmployeeMonthlySalary = x.EmployeeMonthlySalary
                                                 }).ToList();

            return FullDesc;
        }


        // ********  filter by LastName
        public IEnumerable<FullTimeDetailsExtensions> FindFullEmployeeBy(string predicate)
        {
            var FullDescFind =
                                                (from aa in pr.Employee
                                                 join bb in pr.FullTimeEmployee
                                                 on aa.ID equals bb.EmpId
                                                 where aa.TypeId == 1 && aa.LastName == predicate
                                                 select new
                                                 {
                                                     ID = bb.ID,
                                                     EmpId = aa.ID,
                                                     FirstName = aa.FirstName,
                                                     LastName = aa.LastName,
                                                     EmployeeMonthlySalary = bb.EmployeeMonthlySalary
                                                 }).AsEnumerable().Select(x => new FullTimeDetailsExtensions
                                                 {
                                                     ID = x.ID,
                                                     EmpId = x.EmpId,
                                                     FirstName = x.FirstName,
                                                     LastName = x.LastName,
                                                     EmployeeMonthlySalary = x.EmployeeMonthlySalary
                                                 }).ToList();

            return FullDescFind;
        }

    }
}
