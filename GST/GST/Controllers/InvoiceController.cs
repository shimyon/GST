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
    public class InvoiceController : BaseApiController
    { 
        
        InvoiceServices service = new InvoiceServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(InvoiceSearch search)
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
        public IHttpActionResult GetById(InvoiceViewModel obj)
        {
            var getInvoice = service.Get(obj.Id);
            return Ok(getInvoice);
        }

        [HttpPost]
        public IHttpActionResult AddData(invoice invoiceobj)
        {
            AuthDetails authdet = LoginUserDetails();
            invoiceobj.UpdatedBy = authdet.UserId;
            invoiceobj.CreatedBy = authdet.UserId;
            var result = service.Add(invoiceobj);
            return Ok(result);
        }
    }
}