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
    public class ContactController : BaseApiController
    {
        ContactServices service = new ContactServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(ContactSearch search)
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
        public IHttpActionResult GetById(ContactViewModel obj)
        {
            var getContact = service.Get(obj.Id);
            return Ok(getContact);
        }

        [HttpPost]
        public IHttpActionResult AddData(contact contactobj)
        {
            AuthDetails authdet = LoginUserDetails();
            contactobj.UpdatedBy = authdet.UserId;
            contactobj.CreatedBy = authdet.UserId;
            var result = service.Add(contactobj);
            return Ok(result);
        }
    }
}