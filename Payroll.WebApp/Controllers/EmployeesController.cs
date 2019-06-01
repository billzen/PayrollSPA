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
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiControllerBase
    {


        private readonly IEntityBaseRepository<Employee> _employeesRepository;
        private readonly IEntityBaseRepository<TimeCard> _timecardRepository;



        public EmployeesController(IEntityBaseRepository<Employee> employeesRepository, 
            IEntityBaseRepository<TimeCard> timecardRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)

        {
            _employeesRepository = employeesRepository;
            _timecardRepository = timecardRepository;

        }



        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var employees = _employeesRepository.GetAll().Take(6).ToList(); /*OrderByDescending(m => m.ID)*/

                IEnumerable<EmployeeViewModel> employeesVM = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                response = request.CreateResponse<IEnumerable<EmployeeViewModel>>(HttpStatusCode.OK, employeesVM);

                return response;
            });
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Employee> employees = null;
                int totalEmployees = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    employees = _employeesRepository.FindBy(c => c.LastName.ToLower().Contains(filter) ||   
                            c.FirstName.ToLower().Contains(filter))
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalEmployees = _employeesRepository.GetAll()
                        .Where(c => c.LastName.ToLower().Contains(filter) ||
                            c.FirstName.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    employees = _employeesRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalEmployees = _employeesRepository.GetAll().Count();
                }

                IEnumerable<EmployeeViewModel> employeesVM = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                PaginationSet<EmployeeViewModel> pagedSet = new PaginationSet<EmployeeViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalEmployees,
                    TotalPages = (int)Math.Ceiling((decimal)totalEmployees / currentPageSize),
                    Items = employeesVM
                };

                response = request.CreateResponse<PaginationSet<EmployeeViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }



        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(HttpRequestMessage request, EmployeeViewModel employee)
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
                    if (_employeesRepository.EmployeeExists(employee.FirstName, employee.LastName))
                    {
                        ModelState.AddModelError("Invalid Employee", "First - Last Name combination already exists on other Employee");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Employee newEmployee = new Employee();

                        //******************* Add TimeCard to new Employee
                        TimeCardManage timecard = new TimeCardManage(_timecardRepository, _errorsRepository, _unitOfWork);
                        try
                        {
                            
                            timecard.AddTimeCard();
                           
                            //*****************************

                        }
                        catch (Exception ex)
                        {
                            response = request.CreateResponse<EmployeeViewModel>(HttpStatusCode.NotImplemented, employee, ex.ToString() + "/n" +  timecard.AddTimeCard());
                        }

                        employee.TimeCardId = timecard.SelectMaxTimeCardId();
                        newEmployee.UpdateEmployee(employee);

                        _employeesRepository.Add(newEmployee);

                        _unitOfWork.Commit();

                        // Update view model
                        employee = Mapper.Map<Employee, EmployeeViewModel>(newEmployee);
                        response = request.CreateResponse<EmployeeViewModel>(HttpStatusCode.Created, employee);

                    }
                }

                return response;
            });
        }



        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, EmployeeViewModel employee)
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
                    Employee _employee = _employeesRepository.GetSingle(employee.ID);
                    _employee.UpdateEmployee(employee);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }




       // *********** Not used in FullTime.performance errors.Keep for future use eith only one reponse
        [AllowAnonymous]
        [Route("GetDetails")]
        public HttpResponseMessage GetDetails(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Employee _employee = _employeesRepository.GetSingle(Id);

                response = request.CreateResponse(HttpStatusCode.OK, _employee);

                return response;
            });
        }
        #region Private methods

        #endregion

    }
}
