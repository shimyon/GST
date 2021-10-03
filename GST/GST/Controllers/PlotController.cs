using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services;
using services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class PlotController : BaseApiController
    {
        PlotServices service = new PlotServices();
        DatatableService datatableService = new DatatableService();
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
    }

}