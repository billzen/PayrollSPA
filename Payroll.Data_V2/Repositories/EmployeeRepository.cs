using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Objects;
using System.Linq.Expressions;
using Payroll.Entities;
using Payroll.Data.Infrastructure;

namespace Payroll.Data.Repositories
{
    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory) { }


        //public readonly PayrollContext DbContext = new PayrollContext();

        //public IQueryable<Employee> GetAllEmployee()
        //{
        //    IQueryable<Employee> Emp = from aa in DbContext.Employee select aa;  //orderby aa.EmpId ascending
        //    return Emp;
        //}


        //public IQueryable<object> GetAllEmployeeWithTypeDescription()
        //{

        //    //var b = from bb in DbContext.EmployeeType select bb ;

        //    IQueryable<object> EmpTypeDesc = from aa in DbContext.Employee
        //                                     join bb in DbContext.EmployeeType
        //                                     on aa.TypeId equals bb.ID
        //                                     select new
        //                                     {
        //                                         aa.ID,
        //                                         aa.FirstName,
        //                                         aa.LastName,
        //                                         aa.Address,
        //                                         aa.City,
        //                                         aa.PostCode,
        //                                         aa.Phone,
        //                                         aa.Email,
        //                                         bb.TypeDescription,
        //                                         aa.TimeCardId
        //                                     };
        //    return EmpTypeDesc;
        //}


        //public IQueryable<Employee> GetEmployeeById(int EmpId)
        //{
        //    IQueryable<Employee> Emp = from aa in DbContext.Employee where aa.ID == EmpId select aa;
        //    return Emp;
        //}


        //public IQueryable<EmployeeType> GetEmployeeType()
        //{
        //    IQueryable<EmployeeType> EmpType = from aa in DbContext.EmployeeType select aa;
        //    return EmpType;
        //}

        //public IQueryable<EmployeeType> GetEmployeeTypeById(int EmpTypeId)
        //{
        //    IQueryable<EmployeeType> EmpType = from aa in DbContext.EmployeeType where aa.ID == EmpTypeId select aa;
        //    return EmpType;
        //}


        //public int SelectMaxEmployeeId()
        //{
        //    IQueryable<Employee> MaxEmployeeid = from aa in DbContext.Employee select aa;

        //    return MaxEmployeeid.Max(p => p.ID);
        //}


        //public IQueryable<Employee> SearchEmployeeByLastname(String LastName)
        //{
        //    IQueryable<Employee> Emp = from aa in DbContext.Employee where aa.LastName.Contains(LastName) select aa;
        //    return Emp;
        //}


        //public void AddEmployee(Employee NewEmployee)
        //{
        //    DbContext.Employee.Add(NewEmployee);
        //    DbContext.SaveChanges();
        //}

        //public void EditEmployee(Employee UpdateEmployee)
        //{
        //    DbContext.Entry(UpdateEmployee).State = System.Data.Entity.EntityState.Modified; //********************************???
        //    DbContext.SaveChanges();
        //}


    }
}
