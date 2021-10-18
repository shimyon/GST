﻿using models.DatabaseTable;
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
    public class PlotController : BaseApiController
    {
        PlotServices service = new PlotServices();
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();

        [HttpPost]
        public IHttpActionResult GetList(PlotSearch search)
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
        public IHttpActionResult PlotIDDropDownAll(int Id)
        {
            var getPlot = service.PlotIDDropDownAll(Id);
            return Ok(getPlot);
        }

        [HttpPost]
        public IHttpActionResult GetById(PlotViewModel obj)
        {
            var getPlot = service.Get(obj.Id);
            return Ok(getPlot);
        }

        [HttpPost]
        public IHttpActionResult AddData(plot plotobj)
        {
            AuthDetails authdet = LoginUserDetails();
            plotobj.UpdatedBy = authdet.UserId;
            plotobj.CreatedBy = authdet.UserId;
            var result = service.Add(plotobj);
            return Ok(result);
        }


        [HttpPost]
        public HttpResponseMessage DocumentDownload(plot plotObj)
        {
            var example_html = "<html><body></body></html>";
            var example_css = @".headline{font-size:200%}";
            string filename = "Sample";
            if (plotObj.DocumentType == "AllotmentLetter")
            {
                var result = service.DownloadAllotmentLetter(plotObj);
                example_html = "<html><body>" + result + "</body></html>";
                example_css = @".headline{font-size:200%}";
            }
            else if (plotObj.DocumentType == "Banakhat")
            {
                var result = service.DownloadBanakhat(plotObj);
                example_html = "<html><body>" + result + "</body></html>";
                example_css = @".headline{font-size:200%}";
            }
            else if (plotObj.DocumentType == "Sale Deed")
            {
                var result = service.DownloadBanakhat(plotObj);
                example_html = "<html><body>" + result + "</body></html>";
                example_css = @".headline{font-size:200%}";
            }
            byte[] buffer = commsrv.PdfGenerate(example_html, example_css);
            HttpResponseMessage response = PDFResponse(filename, buffer);
            return response;
        }
    }

}