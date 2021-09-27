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
    public class PaymentController : BaseApiController
    {
        PaymentServices service = new PaymentServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(PaymentSearch search)
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
        public IHttpActionResult GetById(PaymentViewModel obj)
        {
            var getPayment = service.Get(obj.Id);
            return Ok(getPayment);
        }

        [HttpPost]
        public IHttpActionResult AddData(payment paymentobj)
        {
            AuthDetails authdet = LoginUserDetails();
            paymentobj.UpdatedBy = authdet.UserId;
            paymentobj.CreatedBy = authdet.UserId;
            var result = service.Add(paymentobj);
            return Ok(result);
        }
    }

}