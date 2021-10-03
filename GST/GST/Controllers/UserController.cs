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

namespace GST.Controllers
{
    public class UserController : BaseApiController
    {
        UserService service = new UserService();
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();
        [HttpPost]
        public IHttpActionResult GetUserList(UserSearch search)
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
        public IHttpActionResult AddData(user userobj)
        {

            AuthDetails authdet = LoginUserDetails();
            userobj.CreatedBy = authdet.UserId;
            userobj.UpdatedBy = authdet.UserId;
            var result = service.Add(userobj);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetById(user userobj)
        {
            var result = service.Get(userobj.Id);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Edit(user userobj)
        {
            var result = service.Edit(userobj);
            return Ok(result);
        }

    }
}
