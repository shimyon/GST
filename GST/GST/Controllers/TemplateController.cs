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
        public IHttpActionResult GetTemplate(TemplateViewModel login)
        {
            AuthDetails authDet = LoginUserDetails();
            Int32 userid = authDet.UserId;
            var getTemplate = service.Get(userid, login.TemplateFor);
            return Ok(getTemplate);
        }
    }
}
