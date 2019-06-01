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
    [RoutePrefix("api/FullTimeEmployees")]
    public class FullTimeEmployeesController : ApiControllerBase
    {

        private readonly IEntityBaseRepository<FullTimeEmployee> _fullTimeemployeeRepository;

        public FullTimeEmployeesController(IEntityBaseRepository<FullTimeEmployee> fullTimeemployeeRepository,
            IEntityBaseRepository<TimeCard> timecardRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)

        {
            _fullTimeemployeeRepository = fullTimeemployeeRepository;

        }




        ////******************************* Get Method with no First & Last Name Employee 
        //[HttpGet]
        //[Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        //public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        //{
        //    int currentPage = page.Value;
        //    int currentPageSize = pageSize.Value;

        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        List<FullTimeEmployee> fulltimeemployees = null;
        //        int totalFullTimeEmployees = new int();

        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            filter = filter.Trim().ToLower();

        //            fulltimeemployees = _fullTimeemployeeRepository.FindBy(c => c.ID == int.Parse(filter))
        //                .OrderBy(c => c.ID)
        //                .Skip(currentPage * currentPageSize)
        //                .Take(currentPageSize)
        //                .ToList();

        //            totalFullTimeEmployees = _fullTimeemployeeRepository.GetAll()
        //                .Count();
        //        }
        //        else
        //        {
        //            fulltimeemployees = _fullTimeemployeeRepository.GetAll()
        //             .OrderBy(c => c.ID)
        //             .Skip(currentPage * currentPageSize)
        //             .Take(currentPageSize)
        //         .ToList();

        //            totalFullTimeEmployees = _fullTimeemployeeRepository.GetAll().Count();
        //        }

        //        IEnumerable<FullTimeEmployeeViewModel> fulltimeemployeesVM = Mapper.Map<IEnumerable<FullTimeEmployee>, IEnumerable<FullTimeEmployeeViewModel>>(fulltimeemployees);

        //        PaginationSet<FullTimeEmployeeViewModel> pagedSet = new PaginationSet<FullTimeEmployeeViewModel>()
        //        {
        //            Page = currentPage,
        //            TotalCount = totalFullTimeEmployees,
        //            TotalPages = (int)Math.Ceiling((decimal)totalFullTimeEmployees / currentPageSize),
        //            Items = fulltimeemployeesVM
        //        };

        //        response = request.CreateResponse<PaginationSet<FullTimeEmployeeViewModel>>(HttpStatusCode.OK, pagedSet);

        //        return response;
        //    });
        //}


        ////**************** Get Method with First & Last Name Employee using FullTimeDetailsExtensions class and methods
        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            FullTimeDetailsExtensions fullTimeDetails = new FullTimeDetailsExtensions();
            
            return CreateHttpResponse(request, () => 
            {
                HttpResponseMessage response = null;
                List<FullTimeDetailsExtensions> fulltimeemployees = null;
                int totalFullTimeEmployees = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    fulltimeemployees = fullTimeDetails.FindFullEmployeeBy(filter)
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalFullTimeEmployees = fullTimeDetails.GetAllFullTimeEmployeesWithTypeDescription()
                        .Count();
                }
                else
                {
                    //  fulltimeemployees = _fullTimeemployeeDetailsRepository.GetAllFullTimeEmployeesWithTypeDescription()
                    fulltimeemployees = fullTimeDetails.GetAllFullTimeEmployeesWithTypeDescription()
                    .OrderBy(c => c.ID)
                     .Skip(currentPage * currentPageSize)
                     .Take(currentPageSize)
                 .ToList();

                    totalFullTimeEmployees = fullTimeDetails.GetAllFullTimeEmployeesWithTypeDescription().Count();
                }

                IEnumerable<FullTimeEmployeeDetailViewModel> fulltimeemployeesVM = Mapper.Map<IEnumerable<FullTimeDetailsExtensions>, IEnumerable<FullTimeEmployeeDetailViewModel>>(fulltimeemployees);

                PaginationSet<FullTimeEmployeeDetailViewModel> pagedSet = new PaginationSet<FullTimeEmployeeDetailViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalFullTimeEmployees,
                    TotalPages = (int)Math.Ceiling((decimal)totalFullTimeEmployees / currentPageSize),
                    Items = fulltimeemployeesVM
                };

                response = request.CreateResponse<PaginationSet<FullTimeEmployeeDetailViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, FullTimeEmployeeViewModel fulltimeemployee)
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
                    FullTimeEmployee _fulltimeemployee = _fullTimeemployeeRepository.GetSingle(fulltimeemployee.ID);
                    _fulltimeemployee.UpdateFullTimeEmployee(fulltimeemployee);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }

}
