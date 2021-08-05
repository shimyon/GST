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

namespace GST.Controllers
{
    public class TemplateController : BaseApiController
    {
        TemplateService service = new TemplateService();
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();
        [HttpPost]
        public IHttpActionResult GetList(TemplateSearch search)
        {
            AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
            };
            var result = service.GetList(search, filters);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult templateDropDownAll()
        {
            var getTemplate = service.templateDropDownAll();
            return Ok(getTemplate);
        }

        [HttpPost]
        public IHttpActionResult GetById(TemplateViewModel obj)
        {
            var getTemplate = service.Get(obj.Id);
            return Ok(getTemplate);
        }

        [HttpPost]
        public IHttpActionResult AddData(template templateobj)
        {
            AuthDetails authdet = LoginUserDetails();
            templateobj.userid = authdet.UserId;
            templateobj.CreatedBy = authdet.UserId;
            templateobj.UpdatedBy = authdet.UserId;
            var result = service.Add(templateobj);
            return Ok(result);
        }

        [HttpGet]
        public HttpResponseMessage SamplePDF()
        {
            string filename = "Sample";
            var example_html = @"<p>This <em>is </em><span class=""headline"" style=""text-decoration: underline;"">some</span> <strong>sample <em> text</em></strong><span style=""color: red;"">!!!</span></p>";
            var example_css = @".headline{font-size:200%}";
            byte[] buffer = commsrv.PdfGenerate(example_html, example_css);
            HttpResponseMessage response = PDFResponse(filename, buffer);
            return response;
        }
    }
}
