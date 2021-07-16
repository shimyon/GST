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
            var getProduct = service.Get(obj.Id);
            return Ok(getProduct);
        }

        [HttpPost]
        public IHttpActionResult AddData(template productobj)
        {
            var result = service.Add(productobj);
            return Ok(result);
        }
    }
}
