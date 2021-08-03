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
    public class QuotationController : BaseApiController
    { 
        Quotation_itemsServices service = new Quotation_itemsServices();
       
        [HttpPost]
        public IHttpActionResult GetById(Quotation_itemsViewModel obj)
        {
            var getQuotation_items = service.Get(obj.Id);
            return Ok(getQuotation_items);
        }

        [HttpPost]
        public IHttpActionResult AddData(quotation_items quotation_itemsobj)
        {
            var result = service.Add(quotation_itemsobj);
            return Ok(result);
        }

        QuotationServices service1 = new QuotationServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(QuotationSearch search)
        {
            AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
            };
            var result = service1.GetList(search, filters);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetUsingID(QuotationViewModel obj)
        {
            var getQuotation = service1.Get(obj.Id);
            return Ok(getQuotation);
        }

        [HttpPost]
        public IHttpActionResult AddQuotData(quotation quotationobj)
        {
            AuthDetails authdet = LoginUserDetails();
            quotationobj.UpdatedBy = authdet.UserId;
            quotationobj.CreatedBy = authdet.UserId;
            var result = service1.Add(quotationobj);
            return Ok(result);
        }
    }
}