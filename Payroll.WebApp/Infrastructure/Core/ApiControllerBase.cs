using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Payroll.Data.Infrastructure;
using Payroll.Data.Repositories;
using Payroll.Entities;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using Payroll.WebApp.Infrastructure.Extensions;

using System.Threading.Tasks;

namespace Payroll.WebApp.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IEntityBaseRepository<Error> _errorsRepository;

        protected readonly IUnitOfWork _unitOfWork;

        public ApiControllerBase(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            _errorsRepository = errorsRepository;
            _unitOfWork = unitOfWork;
        }

        public ApiControllerBase(IDataRepositoryFactory dataRepositoryFactory, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            _errorsRepository = errorsRepository;
            _unitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        /// <summary>
        ///  Async calling for getting data . On top add --using System.Threading.Tasks;-- 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="function"></param>
        /// <returns>On success return async Func<Task<HttpResponseMessage>> function. See Implementation on PartTimeEmployeesController </returns>
        protected async Task<HttpResponseMessage> CreatecHttpResponseAsync(HttpRequestMessage request, Func<Task<HttpResponseMessage>> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                  response =  request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);

            }
            catch (Exception ex)
            {
                LogError(ex);
                response =  request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }



        private void LogError(Exception ex)
        {
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now
                };

                _errorsRepository.Add(_error);
                _unitOfWork.Commit();
            }
            catch { }
        }
    }
}
