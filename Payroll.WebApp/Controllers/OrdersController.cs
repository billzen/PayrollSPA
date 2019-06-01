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
using Newtonsoft.Json.Linq;

namespace Payroll.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiControllerBase
    {

        private readonly IEntityBaseRepository<Order> _ordersRepository;
        private readonly IEntityBaseRepository<EmployeeOrder> _employeeordersRepository;


        public OrdersController(IEntityBaseRepository<Order> ordersRepository,
            IEntityBaseRepository<EmployeeOrder> employeeordersRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _employeeordersRepository = employeeordersRepository;
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Order> orders = null;
                int totalOrders = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    orders = _ordersRepository.FindBy(c => c.Orderdescription.ToLower().Contains(filter))
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalOrders = _ordersRepository.GetAll()
                        .Where(c => c.Orderdescription.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    orders = _ordersRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalOrders = _ordersRepository.GetAll().Count();
                }

                IEnumerable<OrderViewModel> ordersVM = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

                PaginationSet<OrderViewModel> pagedSet = new PaginationSet<OrderViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalOrders,
                    TotalPages = (int)Math.Ceiling((decimal)totalOrders / currentPageSize),
                    Items = ordersVM
                };

                response = request.CreateResponse<PaginationSet<OrderViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, OrderViewModel order)
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
                    Order _order = _ordersRepository.GetSingle(order.ID);
                    _order.UpdateOrder(order);
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }


        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, OrderViewModel order)
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

                        Order newOrder = new Order();
                        newOrder.UpdateOrder(order);
                        _ordersRepository.Add(newOrder);
                        _unitOfWork.Commit();
                        // Update view model
                        order = Mapper.Map<Order, OrderViewModel>(newOrder);
                        response = request.CreateResponse<OrderViewModel>(HttpStatusCode.Created, order);

                }
                return response;
            });
        }


        [HttpGet]
        [Route("GetOrdersForEmployee")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var orders = _ordersRepository.GetAll().OrderByDescending(m => m.ID).ToList();

                IEnumerable<OrderViewModel> ordersVM = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

                response = request.CreateResponse<IEnumerable<OrderViewModel>>(HttpStatusCode.OK, ordersVM);

                return response;
            });
        }

        //[HttpPost]
        //[Route("AddOrdersToEmployee")]
        //public HttpResponseMessage AddOrdersToEmployee(HttpRequestMessage request, int employeeId, int[] orders)
        //{

        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest,
        //                ModelState.Keys.SelectMany(k => ModelState[k].Errors)
        //                      .Select(m => m.ErrorMessage).ToArray());
        //        }
        //        else
        //        {

        //            foreach (var orderid in orders)
        //            {
        //                EmployeeOrderViewModel employeeorderVM = new EmployeeOrderViewModel();
        //                EmployeeOrder newemployeeorder = new EmployeeOrder();
        //                newemployeeorder.OrderId = orderid;
        //                newemployeeorder.CommissionedEmployeeId = employeeId;
        //                _employeeordersRepository.Add(newemployeeorder);
        //                _unitOfWork.Commit();
        //           //   employeeorderVM = Mapper.Map<newemployeeorder, employeeorderVM>(employeeorderVM);

        //            }

        //            response = request.CreateResponse<OrderViewModel>(HttpStatusCode.Created, new OrderViewModel());
        //        }
        //        return response;
        //    });

        //}

        [HttpPost]
        [Route("AddOrdersToEmployee")]
        public HttpResponseMessage AddOrdersToEmployee(HttpRequestMessage request, JObject objData) 
        {
          //**** SOURCE URL SAMPLE:  http://www.dotnetcurry.com/aspnet/1278/aspnet-webapi-pass-multiple-parameters-action-method
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
                    dynamic jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject(objData.ToString()) ;
                    var employeeIdJson = jsonData.jsonObj.employeeId;
                    var ordersJson = jsonData.jsonObj.orders;

                    List<EmployeeOrder> EmployeeOrderobj = new List<EmployeeOrder>();

                    foreach (var orderid in ordersJson)
                    {
                        EmployeeOrderViewModel employeeorderVM = new EmployeeOrderViewModel();
                        EmployeeOrder newemployeeorder = new EmployeeOrder();
                        newemployeeorder.OrderId = orderid.Id;
                        newemployeeorder.CommissionedEmployeeId = employeeIdJson;
                        _employeeordersRepository.Add(newemployeeorder);
                        _unitOfWork.Commit();

                         EmployeeOrderobj.Add(newemployeeorder);                       
                    }
                     IEnumerable<EmployeeOrderViewModel> EmployeeordersVM = Mapper.Map<IEnumerable<EmployeeOrder>, IEnumerable<EmployeeOrderViewModel>>(EmployeeOrderobj);
                    response = request.CreateResponse<IEnumerable<EmployeeOrderViewModel>>(HttpStatusCode.OK, EmployeeordersVM);
                }
                return response;
            });

        }


        //[HttpGet]
        //[Route("OrderEmployees")]
        //public HttpResponseMessage OrderEmployees(HttpRequestMessage request, int orderid)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //      
        //        List<int> employeeorderVM = new List<int>();
        //        HttpResponseMessage response = null;
        //        var orderemployees = _employeeordersRepository.GetAll().Where(ord => ord.OrderId == orderid).Select(emp => emp.CommissionedEmployeeId).ToList();

        //        response = request.CreateResponse<IEnumerable<int>>(HttpStatusCode.OK, orderemployees);

        //        return response;
        //    });
        //}


        [HttpGet]
        [Route("OrderEmployees")]
        public HttpResponseMessage OrderEmployees(HttpRequestMessage request, int orderid)
        {

            CommissionedDetailsExtension commissionedDetailsExtension = new CommissionedDetailsExtension();
            List<CommissionedDetailsExtension> commissionedemployees = new List<CommissionedDetailsExtension>();

            return CreateHttpResponse(request, () =>
            {

               // List<int> employeeorderVM = new List<int>();
                HttpResponseMessage response = null;
                var orderemployees = _employeeordersRepository.GetAll().Where(ord => ord.OrderId == orderid).Select(emp => emp.CommissionedEmployeeId).ToList();


                foreach (var commissioneddetail in orderemployees)
                {
                    commissionedemployees.Add(commissionedDetailsExtension.SelectById(commissioneddetail));
                } 

                response = request.CreateResponse<IEnumerable<CommissionedDetailsExtension>>(HttpStatusCode.OK, commissionedemployees);

                return response;
            });
        }

    }
}
