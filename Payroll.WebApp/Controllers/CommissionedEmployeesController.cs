using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Payroll.Data.Infrastructure;
using Payroll.Data.Repositories;
using Payroll.Entities;
using Payroll.WebApp.Infrastructure.Core;
using Payroll.WebApp.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Payroll.WebApp.Infrastructure.Extensions;
using Payroll.Data.Extensions;
using Payroll.WebApp.BushinessProcesses;

namespace Payroll.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/CommissionedEmployees")]
    public class CommissionedEmployeesController : ApiControllerBase
    {

        private readonly IEntityBaseRepository<CommissionedEmployee> _commissionedemployeeRepository;
        private readonly IEntityBaseRepository<EmployeeOrder> _employeeordersRepository;

        public CommissionedEmployeesController(IEntityBaseRepository<CommissionedEmployee> commissionedemployeeRepository,
          IEntityBaseRepository<EmployeeOrder> employeeordersRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)

        {
            _commissionedemployeeRepository = commissionedemployeeRepository;
            _employeeordersRepository = employeeordersRepository;
        }


        //[HttpGet]
        //[Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        //public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        //{
        //    int currentPage = page.Value;
        //    int currentPageSize = pageSize.Value;

        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        List<CommissionedEmployee> commissionedemployees = null;
        //        int totalCommissionedEmployees = new int();

        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            filter = filter.Trim().ToLower();

        //            commissionedemployees = _commissionedemployeeRepository.FindBy(c => c.ID == int.Parse(filter))
        //                .OrderBy(c => c.ID)
        //                .Skip(currentPage * currentPageSize)
        //                .Take(currentPageSize)
        //                .ToList();

        //            totalCommissionedEmployees = _commissionedemployeeRepository.GetAll()
        //                .Count();
        //        }
        //        else
        //        {
        //            commissionedemployees = _commissionedemployeeRepository.GetAll()
        //                .OrderBy(c => c.ID)
        //                .Skip(currentPage * currentPageSize)
        //                .Take(currentPageSize)
        //            .ToList();

        //            totalCommissionedEmployees = _commissionedemployeeRepository.GetAll().Count();
        //        }

        //        IEnumerable<CommissionedEmployeeViewModel> commissionedemployeesVM = Mapper.Map<IEnumerable<CommissionedEmployee>, IEnumerable<CommissionedEmployeeViewModel>>(commissionedemployees);

        //        PaginationSet<CommissionedEmployeeViewModel> pagedSet = new PaginationSet<CommissionedEmployeeViewModel>()
        //        {
        //            Page = currentPage,
        //            TotalCount = totalCommissionedEmployees,
        //            TotalPages = (int)Math.Ceiling((decimal)totalCommissionedEmployees / currentPageSize),
        //            Items = commissionedemployeesVM
        //        };

        //        response = request.CreateResponse<PaginationSet<CommissionedEmployeeViewModel>>(HttpStatusCode.OK, pagedSet);

        //        return response;
        //    });
        //}


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            CommissionedDetailsExtension commissionedDetailsExtension = new CommissionedDetailsExtension(); 

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CommissionedDetailsExtension> commissionedemployees = null;
                int totalCommissionedEmployees = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    commissionedemployees = commissionedDetailsExtension.FindCommissionedEmployeeBy(filter)
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalCommissionedEmployees = commissionedDetailsExtension.GetAllCommissionedEmployeesWithTypeDescription()
                        .Count();
                }
                else
                {
                    commissionedemployees = commissionedDetailsExtension.GetAllCommissionedEmployeesWithTypeDescription()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalCommissionedEmployees = commissionedDetailsExtension.GetAllCommissionedEmployeesWithTypeDescription().Count();
                }

                IEnumerable<CommissionedEmployeeDetailViewModel> commissionedemployeesVM = Mapper.Map<IEnumerable<CommissionedDetailsExtension>, IEnumerable<CommissionedEmployeeDetailViewModel>>(commissionedemployees);

                PaginationSet<CommissionedEmployeeDetailViewModel> pagedSet = new PaginationSet<CommissionedEmployeeDetailViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalCommissionedEmployees,
                    TotalPages = (int)Math.Ceiling((decimal)totalCommissionedEmployees / currentPageSize),
                    Items = commissionedemployeesVM
                };

                response = request.CreateResponse<PaginationSet<CommissionedEmployeeDetailViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CommissionedEmployeeViewModel commissionedemployee)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    CommissionedEmployee _commissionedemployee = _commissionedemployeeRepository.GetSingle(commissionedemployee.ID);
                    _commissionedemployee.UpdateCommissionedEmployee(commissionedemployee);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpGet]
        [Route("GetEmployeeCurrentOrders/{employeeID:int=0}")]
        public HttpResponseMessage GetEmployeeCurrentOrders(HttpRequestMessage request, int employeeID)
        {
            return CreateHttpResponse(request, () =>
            {
                //int CommissionEmployeeId*/ = 7;
                List<int> employeeorderVM = new List<int>();
                HttpResponseMessage response = null;
                var currentemployeeorders = _employeeordersRepository.GetAll().Where(emp => emp.CommissionedEmployeeId == employeeID).Select(ord => ord.OrderId).ToList();
                
                response = request.CreateResponse<IEnumerable<int>>(HttpStatusCode.OK, currentemployeeorders);

                return response;
            });
        }

    }
}
