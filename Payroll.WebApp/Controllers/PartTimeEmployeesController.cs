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
using Payroll.WebApp.Infrastructure.Extensions;
using Payroll.Data.Extensions;

using System.Threading.Tasks;
using System.Data.Entity;

namespace Payroll.WebApp.Controllers
{

    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/PartTimeEmployees")]
    public class PartTimeEmployeesController : ApiControllerBase
    {

        private readonly IEntityBaseRepository<PartTimeEmployee> _parttimeemployeeRepository;

        public PartTimeEmployeesController(IEntityBaseRepository<PartTimeEmployee> parttimeemployeeRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork) : base(_errorsRepository, _unitOfWork)
        {
            _parttimeemployeeRepository = parttimeemployeeRepository;
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
        //        List<PartTimeEmployee> parttimeemployees = null;
        //        int totalEmployees = new int();

        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            filter = filter.Trim().ToLower();

        //            parttimeemployees = _parttimeemployeeRepository.FindBy(c => c.ID == Convert.ToInt32(filter))
        //                .OrderBy(c => c.ID)
        //                .Skip(currentPage * currentPageSize)
        //                .Take(currentPageSize)
        //                .ToList();

        //            totalEmployees = _parttimeemployeeRepository.GetAll()
        //                .Where(c => c.ID == Convert.ToInt32(filter))
        //                .Count();
        //        }
        //        else
        //        {
        //            parttimeemployees = _parttimeemployeeRepository.GetAll()
        //                .OrderBy(c => c.ID)
        //                .Skip(currentPage * currentPageSize)
        //                .Take(currentPageSize)
        //            .ToList();

        //            totalEmployees = _parttimeemployeeRepository.GetAll().Count();
        //        }

        //        IEnumerable<PartTimeEmployeeViewModel> parttimeemployeesVM = Mapper.Map<IEnumerable<PartTimeEmployee>, IEnumerable<PartTimeEmployeeViewModel>>(parttimeemployees);

        //        PaginationSet<PartTimeEmployeeViewModel> pagedSet = new PaginationSet<PartTimeEmployeeViewModel>()
        //        {
        //            Page = currentPage,
        //            TotalCount = totalEmployees,
        //            TotalPages = (int)Math.Ceiling((decimal)totalEmployees / currentPageSize),
        //            Items = parttimeemployeesVM
        //        };

        //        response = request.CreateResponse<PaginationSet<PartTimeEmployeeViewModel>>(HttpStatusCode.OK, pagedSet);

        //        return response;
        //    });
        //}

        // http://stackoverflow.com/questions/25902275/async-taskhttpresponsemessage-get-vs-httpresponsemessage-get
        /// <summary>
        ///  Async calling for getting data .
        ///  Add namespaces: System.Threading.Tasks & System.Data.Entity
        ///   call ApiControllerBase.CreatecHttpResponseAsync method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public Task<HttpResponseMessage> Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreatecHttpResponseAsync(request, async () =>   // async lamda method for  await below 
            {
                HttpResponseMessage response = null;
                List<PartTimeEmployee> parttimeemployees = null;
                int totalEmployees = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    parttimeemployees =  await _parttimeemployeeRepository.FindBy(c => c.ID == Convert.ToInt32(filter))
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToListAsync();  // calling async List

                    totalEmployees = _parttimeemployeeRepository.GetAll()
                        .Where(c => c.ID == Convert.ToInt32(filter))
                        .Count();
                }
                else
                {
                    parttimeemployees = await _parttimeemployeeRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToListAsync(); // // calling async List

                    totalEmployees = _parttimeemployeeRepository.GetAll().Count();
                }

                IEnumerable<PartTimeEmployeeViewModel> parttimeemployeesVM = Mapper.Map<IEnumerable<PartTimeEmployee>, IEnumerable<PartTimeEmployeeViewModel>>(parttimeemployees);

                PaginationSet<PartTimeEmployeeViewModel> pagedSet = new PaginationSet<PartTimeEmployeeViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalEmployees,
                    TotalPages = (int)Math.Ceiling((decimal)totalEmployees / currentPageSize),
                    Items = parttimeemployeesVM
                };

                response = request.CreateResponse<PaginationSet<PartTimeEmployeeViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PartTimeEmployeeViewModel parttimeemployee)
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
                    PartTimeEmployee _partTimeEmployee = _parttimeemployeeRepository.GetSingle(parttimeemployee.ID);
                    _partTimeEmployee.UpdatePartTimeEmployee(parttimeemployee);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
             
    }
}
