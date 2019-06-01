using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.Entities;
using Payroll.WebApp.Models;

namespace Payroll.WebApp.Infrastructure.Extensions
{

    /// <summary>
    /// **************  IF IS necessary FILL extensions for your Entities (Employees..............)
    /// </summary>
    public static class EntitiesExtensions
    {


        public static void UpdateEmployee(this Employee employee, EmployeeViewModel employeeVm)
        {
            employee.FirstName = employeeVm.FirstName;
            employee.LastName = employeeVm.LastName;
            employee.Address = employeeVm.Address;
            employee.City = employeeVm.City;
            employee.PostCode = employeeVm.PostCode;
            employee.Phone = employeeVm.Phone;
            employee.Email = employeeVm.Email;
            employee.DepartmentNo = employeeVm.DepartmentNo;
            employee.TypeId = employeeVm.TypeId;
            employee.TimeCardId = employeeVm.TimeCardId;
        }


        public static void UpdateCommissionedEmployee(this CommissionedEmployee commissionedemployee, CommissionedEmployeeViewModel commissionedemployeeVm)
        {

            commissionedemployee.EmployeeMonthlySalary = commissionedemployeeVm.EmployeeMonthlySalary;
            commissionedemployee.MonthlyWorkingHours = commissionedemployeeVm.MonthlyWorkingHours;
            commissionedemployee.WeeeklyPaid = commissionedemployeeVm.WeeeklyPaid;
        }


        public static void UpdateFullTimeEmployee(this FullTimeEmployee fulltimeemployee, FullTimeEmployeeViewModel fulltimeemployeeVm)
        {
            fulltimeemployee.EmployeeMonthlySalary = fulltimeemployeeVm.EmployeeMonthlySalary;
        }


        public static void UpdatePartTimeEmployee(this PartTimeEmployee partTimeEmployee, PartTimeEmployeeViewModel partTimeEmployeeVM)
        {
            partTimeEmployee.PartTimeEmployeeRate = partTimeEmployeeVM.PartTimeEmployeeRate;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVM)
        {
            order.OrderDate = orderVM.OrderDate;
            order.Orderdescription = orderVM.Orderdescription;
            order.OrderAmount = orderVM.OrderAmount;
        }


        public static void UpdateProduct(this Product product, ProductViewModel productVM)
        {
            product.ProductDescription = productVM.ProductDescription;
            product.Price = productVM.Price;
            product.Discount = productVM.Discount;
        }


        public static void UpdateTimeCard(this TimeCard timecard, TimeCardViewModel timecardVM)
        {
            timecard.WorkedHours = 0;
        }

        public static void UpdateLogEntry(this LogEntry logentry, LogEntryViewModel logentryVM)
        {

            logentry.Logdate = logentryVM.Logdate;
            logentry.EntryTime = logentryVM.EntryTime;
            logentry.DepartTime = logentryVM.DepartTime;
            logentry.WorkerdHours = logentryVM.WorkerdHours;
            //*** We excluded the LogEntryImage property from UpdateLogEntry extension cause we will be using a specific FileUpload action to upload images.
            //     logentry.LogEntryImage = logentryVM.LogEntryImage;

        }


    }
}