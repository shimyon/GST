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
    public class UserStaffController : BaseApiController
    {
        UserStaffServices service = new UserStaffServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(UserStaffSearch search)
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
        public IHttpActionResult GetById(UserStaffViewModel obj)
        {
            var getuserStaff = service.Get(obj.Id);
            return Ok(getuserStaff);
        }

        [HttpPost]
        public IHttpActionResult AddData(userStaff userStaffobj)
        {
            AuthDetails authdet = LoginUserDetails();
            userStaffobj.UpdatedBy = authdet.UserId;
            userStaffobj.CreatedBy = authdet.UserId;
            var result = service.Add(userStaffobj);
            return Ok(result);
        }
    }

}