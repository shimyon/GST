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
    public class SiteController : BaseApiController
    {
        SiteServices service = new SiteServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(SiteSearch search)
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
        public IHttpActionResult SiteNameDropDownAll()
        {
            var getSite = service.SiteNameDropDownAll();
            return Ok(getSite);
        }

        [HttpPost]
        public IHttpActionResult GetById(SiteViewModel obj)
        {
            var getSite = service.Get(obj.Id);
            return Ok(getSite);
        }

        [HttpPost]
        public IHttpActionResult AddData(site siteobj)
        {
            AuthDetails authdet = LoginUserDetails();
            siteobj.UpdatedBy = authdet.UserId;
            siteobj.CreatedBy = authdet.UserId;
            var result = service.Add(siteobj);
            return Ok(result);
        }
    }

}