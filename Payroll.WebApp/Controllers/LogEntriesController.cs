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
    [RoutePrefix("api/logentries")]
    public class LogEntriesController : ApiControllerBase
    {

        private readonly IEntityBaseRepository<LogEntry> _logentriesRepository;
        private readonly IEntityBaseRepository<Employee> _employeesRepository;

        public LogEntriesController(IEntityBaseRepository<Employee> employeesRepository,
            IEntityBaseRepository<LogEntry> logentriesRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)

        {
            _employeesRepository = employeesRepository;
            _logentriesRepository = logentriesRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, int filter = 0)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LogEntry> logentries = null;
                int totalLogEntries = new int();

                if (filter > 0 )
                {
                //    filter = filter.Trim().ToLower();

                    logentries = _logentriesRepository.FindBy(c => c.ID == filter)
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalLogEntries = _logentriesRepository.GetAll()
                        .Where(c => c.ID == filter)
                        .Count();
                }
                else
                {
                    logentries = _logentriesRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalLogEntries = _logentriesRepository.GetAll().Count();
                }

                IEnumerable<LogEntryViewModel> logentriesVM = Mapper.Map<IEnumerable<LogEntry>, IEnumerable<LogEntryViewModel>>(logentries);

                PaginationSet<LogEntryViewModel> pagedSet = new PaginationSet<LogEntryViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalLogEntries,
                    TotalPages = (int)Math.Ceiling((decimal)totalLogEntries / currentPageSize),
                    Items = logentriesVM
                };

                response = request.CreateResponse<PaginationSet<LogEntryViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpGet]
        [Route("LogEntryEmployees")]
        public HttpResponseMessage LogEntryEmployees(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var employees = _employeesRepository.GetAll().OrderByDescending(m => m.ID).ToList();

                IEnumerable<EmployeeViewModel> employeesVM = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                response = request.CreateResponse<IEnumerable<EmployeeViewModel>>(HttpStatusCode.OK, employeesVM);

                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, LogEntryViewModel logentry)
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
                    LogEntry _logentry = _logentriesRepository.GetSingle(logentry.ID);
                    if (_logentry == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid LogEntry.");
                    }
                    else
                    {
                        _logentry.UpdateLogEntry(logentry);
                        logentry.LogEntryImage = _logentry.LogEntryImage;
                        _logentriesRepository.Edit(_logentry);
                        _unitOfWork.Commit();

                        response = request.CreateResponse(HttpStatusCode.OK, logentry);
                    }



                }

                return response;
            });
        }

        //*********** We excluded the LogEntryImage property from LogEntryViewModel - EntitiesExtension - LogEntryViewModelValidator cause we will be using a specific FileUpload action to upload images
        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int logentryId)
        {
            
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;

                    var logentryOld = _logentriesRepository.GetSingle(logentryId);
                    if (logentryOld == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid logentry.");
                    else
                    {
                        var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/LogEntries");

                        var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                        // Read the MIME multipart asynchronously 
                        Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                        string _localFileName = multipartFormDataStreamProvider
                            .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                        // Create response
                        FileUploadResult fileUploadResult = new FileUploadResult
                        {
                            LocalFilePath = _localFileName,

                            FileName = Path.GetFileName(_localFileName),

                            FileLength = new FileInfo(_localFileName).Length
                        };

                        // update database
                        logentryOld.LogEntryImage = fileUploadResult.FileName;
                        _logentriesRepository.Edit(logentryOld);
                        _unitOfWork.Commit();

                        response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
                    }

                    return response;
                });
            }



    }
}
