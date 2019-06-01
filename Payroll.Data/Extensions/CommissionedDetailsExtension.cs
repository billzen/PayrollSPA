using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Data.Extensions
{
    /// <summary>
    /// Extended Class for returning CommissionedEmployee with First name & Last Name. Using LING to objects
    /// </summary>
    public class CommissionedDetailsExtension
    {

        private readonly Payroll.Data.PayrollContext pr = new Data.PayrollContext();

        public int ID { get; set; }

        public int EmpId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool WeeeklyPaid { get; set; }

        public decimal MonthlyWorkingHours { get; set; }

        public decimal EmployeeMonthlySalary { get; set; }


        public IEnumerable<CommissionedDetailsExtension> GetAllCommissionedEmployeesWithTypeDescription()
        {
            var CommissionedDesc =
                                                (from aa in pr.Employee
                                                 join bb in pr.CommissionedEmployee
                                                 on aa.ID equals bb.EmpId
                                                 where aa.TypeId == 3
                                                 select new
                                                 {
                                                     ID = bb.ID,
                                                     EmpId = aa.ID,
                                                     FirstName = aa.FirstName,
                                                     LastName = aa.LastName,
                                                     WeeeklyPaid = bb.WeeeklyPaid,
                                                     MonthlyWorkingHours = bb.EmployeeMonthlySalary,
                                                     EmployeeMonthlySalary = bb.EmployeeMonthlySalary
                                                 }).AsEnumerable().Select(x => new CommissionedDetailsExtension
                                                 {
                                                     ID = x.ID,
                                                     EmpId = x.EmpId,
                                                     FirstName = x.FirstName,
                                                     LastName = x.LastName,
                                                     WeeeklyPaid = x.WeeeklyPaid,
                                                     MonthlyWorkingHours = x.MonthlyWorkingHours,
                                                     EmployeeMonthlySalary = x.EmployeeMonthlySalary
                                                 }).ToList();

            return CommissionedDesc;
        }


        public IEnumerable<CommissionedDetailsExtension> FindCommissionedEmployeeBy(string predicate)
        {
            var CommissionedDescFind =
                                              (from aa in pr.Employee
                                               join bb in pr.CommissionedEmployee
                                               on aa.ID equals bb.EmpId
                                               where aa.TypeId == 3 && aa.LastName == predicate
                                               select new
                                               {
                                                   ID = bb.ID,
                                                   EmpId = aa.ID,
                                                   FirstName = aa.FirstName,
                                                   LastName = aa.LastName,
                                                   WeeeklyPaid = bb.WeeeklyPaid,
                                                   MonthlyWorkingHours = bb.EmployeeMonthlySalary,
                                                   EmployeeMonthlySalary = bb.EmployeeMonthlySalary
                                               }).AsEnumerable().Select(x => new CommissionedDetailsExtension
                                               {
                                                   ID = x.ID,
                                                   EmpId = x.EmpId,
                                                   FirstName = x.FirstName,
                                                   LastName = x.LastName,
                                                   WeeeklyPaid = x.WeeeklyPaid,
                                                   MonthlyWorkingHours = x.MonthlyWorkingHours,
                                                   EmployeeMonthlySalary = x.EmployeeMonthlySalary
                                               }).ToList();
            return CommissionedDescFind;
        }

        public CommissionedDetailsExtension SelectById(int CommissionedID)
        {
            var commissionedEmployee =

                                                (from aa in pr.Employee
                                                 join bb in pr.CommissionedEmployee
                                                 on aa.ID equals bb.EmpId
                                                 where aa.TypeId == 3 && bb.ID == CommissionedID
                                                 select new
                                                 {
                                                     ID = bb.ID,
                                                     EmpId = aa.ID,
                                                     FirstName = aa.FirstName,
                                                     LastName = aa.LastName,
                                                     WeeeklyPaid = bb.WeeeklyPaid,
                                                     MonthlyWorkingHours = bb.EmployeeMonthlySalary,
                                                     EmployeeMonthlySalary = bb.EmployeeMonthlySalary
                                                 }).AsEnumerable().Select(x => new CommissionedDetailsExtension
                                                 {
                                                     ID = x.ID,
                                                     EmpId = x.EmpId,
                                                     FirstName = x.FirstName,
                                                     LastName = x.LastName,
                                                     WeeeklyPaid = x.WeeeklyPaid,
                                                     MonthlyWorkingHours = x.MonthlyWorkingHours,
                                                     EmployeeMonthlySalary = x.EmployeeMonthlySalary
                                                 }).FirstOrDefault();

            return commissionedEmployee;
        }


    }
}
