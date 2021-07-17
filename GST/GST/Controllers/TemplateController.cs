using models.DatabaseTable;
using models.ViewModels;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GST.Controllers
{
    public class TemplateController : BaseApiController
    {
        TemplateService service = new TemplateService();

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
