using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Entities;

namespace Payroll.Data.Repositories
{
    public interface IEmployeeRepository: IEntityBaseRepository<Employee>
    {

        ///// <summary>
        ///// Interface for GetAllAmployees
        ///// </summary>
        ///// <returns></returns>
        //IQueryable<Employee> GetAllEmployee();


        ///// <summary>
        ///// Get All the Employee with Type Description column in
        ///// </summary>
        ///// <returns></returns>
        //IQueryable<object> GetAllEmployeeWithTypeDescription();



        ///// <summary>
        ///// Interface for get Employee by EmpId
        ///// </summary>
        ///// <param name="EmpId"></param>
        ///// <returns></returns>
        //IQueryable<Employee> GetEmployeeById(int EmpId);


        ///// <summary>
        ///// Interface for get all EmployeeTypes
        ///// </summary>
        ///// <returns></returns>
        //IQueryable<EmployeeType> GetEmployeeType();

        ///// <summary>
        ///// Interface for get EmployeeType by EmployeeTypeId
        ///// </summary>
        ///// <param name="EmpTypeId"></param>
        ///// <returns></returns>
        //IQueryable<EmployeeType> GetEmployeeTypeById(int EmpTypeId);

        ///// <summary>
        ///// Select Max EmpId from Employee
        ///// </summary>
        ///// <returns></returns>
        //int SelectMaxEmployeeId();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="LastName"></param>
        ///// <returns></returns>
        //IQueryable<Employee> SearchEmployeeByLastname(String LastName);


        //void AddEmployee(Employee NewEmployee);

        //void EditEmployee(Employee UpdateEmployee);

    }
}
