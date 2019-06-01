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

using System.Web.Script.Serialization;
using System.Net.Http.Headers;
using System.Web.Mvc;


namespace Payroll.WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    [System.Web.Http.RoutePrefix("api/FilesHandle")]
    public class FilesHandleController : Controller
    {

        private readonly IEntityBaseRepository<Product> _productsRepository;
        private readonly IEntityBaseRepository<ProductImage> _productImagesRepository;

        public FilesHandleController(IEntityBaseRepository<Product> productsRepository,
            IEntityBaseRepository<ProductImage> productImagesRepository,
         IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
     //    : base(_errorsRepository, _unitOfWork)
        {
            _productsRepository = productsRepository;
            _productImagesRepository = productImagesRepository;
        }

        private byte[] GetImageData(int ProImgid)
        {
            byte[] docbytes = _productImagesRepository.GetAll().Where(aa => aa.ProductId == ProImgid).Select(prod => prod.Filebytes).First();

        //  byte[] docbytes = (from aa in pr.ProductImages where aa.FileId == ProImgid select aa.Filebytes).First();
            return docbytes;
        }


        // http://stackoverflow.com/questions/26038856/how-to-return-a-file-filecontentresult-in-asp-net-webapi
        //http://stackoverflow.com/questions/9541351/returning-binary-file-from-controller-in-asp-net-web-api?rq=1 
        //[System.Web.Http.HttpGet]
        //[Route("ShowImage")]

        //public HttpResponseMessage ShowImage(HttpRequestMessage request, int id, string fname)
        //{

        //        if (string.IsNullOrEmpty(fname))
        //        {
        //            fname = "png";
        //        }
        //        string mime = System.IO.Path.GetFileName(fname);
        //        mime = mime.Replace(@".", @"");
        //        mime = @"image/" + mime;
        //        byte[] bytes = GetImageData(id);
        //        if (bytes == null)
        //        {
        //            return null;
        //        }
        //    // response = request.CreateResponse(HttpStatusCode.OK, System.IO.File.WriteAllBytes(mime, bytes));
        //      return File(bytes, mime);
        //}


       [System.Web.Http.HttpPost]
        //[MimeMultipart]
        [System.Web.Http.Route("images/upload")]
        public HttpResponseMessage Post(HttpRequest Request, int ProductId)
        {

            //     HttpRequestMessage Request = new HttpRequestMessage();

            //return CreateHttpResponse(request, () =>
            //{
                HttpResponseMessage response = null;



            HttpPostedFile file = Request.Files[0] as HttpPostedFile;

            int fileSizeInBytes = file.ContentLength;
            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            byte[] data = target.ToArray();


            //var FileBytes =   Request.Content.ReadAsByteArrayAsync();


            //var content = Request.Content.ReadAsStringAsync().Result;        


            //response = request.CreateResponse(HttpStatusCode.OK, FileBytes);

            //var logentryOld = _logentriesRepository.GetSingle(logentryId);
            //if (logentryOld == null)
            //    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid logentry.");
            //else
            //{
            //    var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/LogEntries");

            //    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

            //    // Read the MIME multipart asynchronously 
            //    Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

            //    string _localFileName = multipartFormDataStreamProvider
            //        .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

            //    // Create response
            //    FileUploadResult fileUploadResult = new FileUploadResult
            //    {
            //        LocalFilePath = _localFileName,

            //        FileName = Path.GetFileName(_localFileName),

            //        FileLength = new FileInfo(_localFileName).Length
            //    };

            //    // update database
            //    logentryOld.LogEntryImage = fileUploadResult.FileName;
            //    _logentriesRepository.Edit(logentryOld);
            //    _unitOfWork.Commit();

            //    response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
            //}

            return response;
            //});
        }



    }
}
