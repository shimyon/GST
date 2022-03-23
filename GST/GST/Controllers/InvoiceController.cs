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
        Invoice_itemsServices service = new Invoice_itemsServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetInvo_itemsList(Invoice_itemsSearch search)
        {
            AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
            };
            var result = service.GetInvo_itemsList(search, filters);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetById(Invoice_itemsViewModel obj)
        {
            var getInvoice_items = service.Get(obj.Id);
            return Ok(getInvoice_items);
        }

        [HttpPost]
        public IHttpActionResult AddData(InvoiceListAdd InvoObj)
        {
            if (InvoObj.invoice_itemsobj != null)
            {
                AuthDetails authdet = LoginUserDetails();
                foreach (var invo in InvoObj.invoice_itemsobj)
                {
                    invo.UpdatedBy = authdet.UserId;
                    invo.CreatedBy = authdet.UserId;
                }
                var result = service.AddItems(InvoObj.invoice_itemsobj);
                return Ok(result);
            }
            else
            {
                return Ok(0);
            }
        }



        InvoiceServices service1 = new InvoiceServices();
        DatatableService datatableService1 = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(InvoiceSearch search)
        {
            AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService1.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
            };
            var result = service1.GetList(search, filters);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetUsingID(InvoiceViewModel obj)
        {
            var getInvoice = service1.Get(obj.Id);
            return Ok(getInvoice);
        }

        [HttpPost]
        public IHttpActionResult AddInvoData(invoice invoiceobj)
        {
            AuthDetails authdet = LoginUserDetails();
            invoiceobj.UpdatedBy = authdet.UserId;
            invoiceobj.CreatedBy = authdet.UserId;
            var result = service1.Add(invoiceobj);
            return Ok(result);
        }

    }
}