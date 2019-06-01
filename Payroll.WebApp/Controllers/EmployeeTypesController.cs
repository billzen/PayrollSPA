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

namespace Payroll.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/employeetypes")]
    public class EmployeeTypesController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<EmployeeType> _employeetypesRepository;
        public EmployeeTypesController(IEntityBaseRepository<EmployeeType> employeetypesRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)

        {
            _employeetypesRepository = employeetypesRepository;
        }


        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var employeetypes = _employeetypesRepository.GetAll().OrderByDescending(m => m.ID).ToList();

                IEnumerable<EmployeeTypeViewModel> employeetypeVM = Mapper.Map<IEnumerable<EmployeeType>, IEnumerable<EmployeeTypeViewModel>>(employeetypes);

                response = request.CreateResponse<IEnumerable<EmployeeTypeViewModel>>(HttpStatusCode.OK, employeetypeVM);

                return response;
            });
        }

    }
}
