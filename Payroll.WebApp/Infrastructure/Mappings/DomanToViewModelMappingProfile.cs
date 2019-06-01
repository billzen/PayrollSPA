using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Payroll.Entities;
using Payroll.WebApp.Models;
using System.Web;
using Payroll.Data.Extensions;

using Payroll.WebApp.Controllers;

namespace Payroll.WebApp.Infrastructure.Mappings
{
    public class DomanToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

        protected override void Configure()
        {

          

            Mapper.CreateMap<Employee, EmployeeViewModel>();

            Mapper.CreateMap<EmployeeType, EmployeeTypeViewModel>();

            Mapper.CreateMap<TimeCard, TimeCardViewModel>();

            Mapper.CreateMap<FullTimeEmployee, FullTimeEmployeeViewModel>();
            //.ForMember(vm => vm.FirstName, map => map.MapFrom(m => m.Employee.FirstName))
            //.ForMember(vm => vm.LastName, map => map.MapFrom(m => m.Employee.LastName));


            // *********** See FullTimeEmployee controlller for this extension 
            Mapper.CreateMap<FullTimeDetailsExtensions, FullTimeEmployeeDetailViewModel>();

            //// *********** See CommissionedEmployee controlller for this extension 
            Mapper.CreateMap<CommissionedDetailsExtension, CommissionedEmployeeDetailViewModel>();


            Mapper.CreateMap<CommissionedEmployee, CommissionedEmployeeViewModel>();

            Mapper.CreateMap<Order, OrderViewModel>();

            Mapper.CreateMap<EmployeeOrder, EmployeeOrderViewModel>();

            Mapper.CreateMap<Product, ProductViewModel>();

            Mapper.CreateMap<LogEntry, LogEntryViewModel>();

            Mapper.CreateMap<ProductImage, ProductImagesViewModel>();

            Mapper.CreateMap<PartTimeEmployee, PartTimeEmployeeViewModel>();


        }

    }
}
