using models.DatabaseTable;
using models.ViewModels;
using services;
using System.Collections.Generic;
using System.Web.Http;
using services.Common;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.IO;
using iTextSharp.text.pdf;
using System.Net.Http.Headers;
using System.Web.Http.Results;
using System.Net;
using iTextSharp.text;
using System;

namespace GST.Controllers
{
    public class PrintController : BaseApiController
    {
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage PrintPDF(PrintViewModel obj)
        {
            //IHttpActionResult
            string filename = "Sample";
            var example_html = obj.TemplateData;
            var example_css = @".headline{font-size:200%}";
            byte[] buffer = commsrv.PdfGenerate(example_html, example_css);
            HttpResponseMessage response = PDFResponse(filename, buffer);
            return response;
            //string pdfBase64= PDFbase64String(buffer);
            //return Ok(pdfBase64);
        }
    }
}
