using models.DatabaseTable;
using models.ViewModels;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class ContactController : BaseApiController
    {
        ContactServices service = new ContactServices();

        [HttpPost]
        public IHttpActionResult GetById(ContactViewModel obj)
        {
            var getContact = service.Get(obj.Id);
            return Ok(getContact);
        }

        [HttpPost]
        public IHttpActionResult AddData(contact contactobj)
        {
            var result = service.Add(contactobj);
            return Ok(result);
        }
    }
}