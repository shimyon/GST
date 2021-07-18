using models.DatabaseTable;
using models.ViewModels;
using services;
using System.Collections.Generic;
using System.Web.Http;
using services.Common;
using MySql.Data.MySqlClient;

namespace GST.Controllers
{
    public class TemplateController : BaseApiController
    {
        TemplateService service = new TemplateService();
        DatatableService datatableService = new DatatableService();
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
    }
}
