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
using System.Net.Http.Headers;

namespace Payroll.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Product> _productsRepository;
        private readonly IEntityBaseRepository<ProductImage> _productImagesRepository;

        public ProductsController(IEntityBaseRepository<Product> productsRepository,
            IEntityBaseRepository<ProductImage> productImagesRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
         : base(_errorsRepository, _unitOfWork)
        {
            _productsRepository = productsRepository;
            _productImagesRepository = productImagesRepository;
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
                List<Product> products = null;
                int totalProducts = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    products = _productsRepository.FindBy(c => c.ProductDescription.ToLower().Contains(filter))
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalProducts = _productsRepository.GetAll()
                        .Where(c => c.ProductDescription.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    products = _productsRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalProducts = _productsRepository.GetAll().Count();
                }

                IEnumerable<ProductViewModel> ordersVM = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

                PaginationSet<ProductViewModel> pagedSet = new PaginationSet<ProductViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalProducts,
                    TotalPages = (int)Math.Ceiling((decimal)totalProducts / currentPageSize),
                    Items = ordersVM
                };

                response = request.CreateResponse<PaginationSet<ProductViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel product)
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
                    Product _product = _productsRepository.GetSingle(product.ID);
                    _product.UpdateProduct(product);
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }



        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductViewModel product)
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

                    Product newProduct = new Product();
                    newProduct.UpdateProduct(product);
                    _productsRepository.Add(newProduct);
                    _unitOfWork.Commit();
                    // Update view model
                    product = Mapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse<ProductViewModel>(HttpStatusCode.Created, product);

                }
                return response;
            });
        }


        [HttpGet]
        [Route("GetProductImages")]
        public HttpResponseMessage GetProductImages(HttpRequestMessage request, int productid)
        {
            return CreateHttpResponse(request, () =>
            {
                List<ProductImage> productImageVM = new List<ProductImage>();
                HttpResponseMessage response = null;
                var productimages = _productImagesRepository.GetAll().Where(primg => primg.ProductId == productid).ToList();

                response = request.CreateResponse<IEnumerable<ProductImage>>(HttpStatusCode.OK, productimages);

                return response;
            });
        }



        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int ProductId)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                else
                {

                    IEnumerable<HttpContent> parts = Request.Content.ReadAsMultipartAsync().Result.Contents;

                    byte[] FileBytes = parts.ToArray()[0].ReadAsByteArrayAsync().Result;
                    string imagefilename = "";
                    int filesize = 0;

                    string conl = parts.ToArray()[0].Headers.ContentLength.ToString();
                    filesize = Convert.ToInt32(conl);

                    foreach (var par in parts.ToArray()[0].Headers.ContentDisposition.Parameters)
                    {
                        if (par.Name == "filename")
                        {
                            string ght = par.Value.ToString();
                            string cutght = ght.Replace("\"", "");
                            imagefilename = cutght;
                        }
                    }

                    ProductImagesViewModel newproductimageVM = new ProductImagesViewModel();

                    ProductImage newproductimage = new ProductImage()
                    {
                        ProductId = ProductId,

                        Filesize = filesize,

                        LogFilename = imagefilename,

                        Filebytes = FileBytes
                    };



                    _productImagesRepository.Add(newproductimage);

                    _unitOfWork.Commit();

                    //*************** Update view model  not neccassery here. ANYWAY....
                    newproductimageVM = Mapper.Map<ProductImage, ProductImagesViewModel>(newproductimage);
                    //response = request.CreateResponse<ProductImagesViewModel>(HttpStatusCode.Created, newproductimageVM);

                    response = request.CreateResponse(HttpStatusCode.OK, newproductimageVM);

                }
                  return response;
           });

        }

        [MimeMultipart]
        public byte[] ReadImagebytes(HttpRequestMessage request)
        {
            IEnumerable<HttpContent> parts = Request.Content.ReadAsMultipartAsync().Result.Contents;

            byte[] FileBytes = parts.ToArray()[0].ReadAsByteArrayAsync().Result;
            string imagefilename = "";
            int ContentLenght = 0;

            string conl = parts.ToArray()[0].Headers.ContentLength.ToString();
            ContentLenght = Convert.ToInt32(conl);

            foreach (var par in parts.ToArray()[0].Headers.ContentDisposition.Parameters)
            {
                    if (par.Name == "filename")
                {
                    string ght = par.Value.ToString();
                    string cutght = ght.Replace("\"", "");
                    imagefilename = cutght;
                }          
            }



            return FileBytes;


        }











    }
}
