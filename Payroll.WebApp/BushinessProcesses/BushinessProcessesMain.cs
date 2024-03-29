﻿using System;
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
using Payroll.WebApp.Infrastructure.Core;

namespace Payroll.WebApp.BushinessProcesses
{
  public class BushinessProcessesMain
    {
        protected readonly IEntityBaseRepository<Error> _errorsRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public BushinessProcessesMain(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            _errorsRepository = errorsRepository;
            _unitOfWork = unitOfWork;
        }

        public BushinessProcessesMain(IDataRepositoryFactory dataRepositoryFactory, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            _errorsRepository = errorsRepository;
            _unitOfWork = unitOfWork;
        }

        //protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        //{
        //    HttpResponseMessage response = null;

        //    try
        //    {
        //        response = function.Invoke();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        LogError(ex);
        //        response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //        response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }

        //    return response;
        //}
        protected internal void LogError(Exception ex)
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
