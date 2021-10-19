using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services;
using services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class PaymentController : BaseApiController
    {
        PaymentServices service = new PaymentServices();
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();
        [HttpPost]
        public IHttpActionResult GetList(PaymentSearch search)
        {
            //AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pPlotId", search.PlotId,  MySqlDbType.Int32)
            };
            var result = service.GetList(search, filters);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetById(PaymentViewModel obj)
        {
            var getPayment = service.Get(obj.Id);
            return Ok(getPayment);
        }

        [HttpPost]
        public IHttpActionResult AddData(payment paymentobj)
        {
            AuthDetails authdet = LoginUserDetails();
            paymentobj.UpdatedBy = authdet.UserId;
            paymentobj.CreatedBy = authdet.UserId;
            var result = service.Add(paymentobj);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Delete(payment obj)
        {
            var objnew = service.Delete(obj.Id);
            return Ok(objnew);
        }

        [HttpPost]
        public HttpResponseMessage DownloadReceipt(payment paymentobj)
        {
            var result = service.DownloadReceipt(paymentobj);
            string filename = "Sample";
            var example_html = "<html><body>" + result + "</body></html>";
            //example_html = "<h1>Test</h1>";
            var example_css = "";
            byte[] buffer = commsrv.PdfGenerate(example_html, example_css);
            HttpResponseMessage response = PDFResponse(filename, buffer);
            return response;

            //string pdfBase64 = PDFbase64String(buffer);
            //return Ok(pdfBase64);
        }


    }

}