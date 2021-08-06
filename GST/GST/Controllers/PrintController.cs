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

        [HttpPost]
        public IHttpActionResult GetById(PrintViewModel obj)
        {
            //var getPrint = service.Get(obj.Id);
            //return Ok(getPrint);
            return Ok();
        }
    }
}
