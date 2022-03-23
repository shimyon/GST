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
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetQuot_itemsList(Quotation_itemsSearch search)
        {
            AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
            };
            var result = service.GetQuot_itemsList(search, filters);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetById(Quotation_itemsViewModel obj)
        {
            var getQuotation_items = service.Get(obj.Id);
            return Ok(getQuotation_items);
        }

        [HttpPost]
        public IHttpActionResult AddData(QuotataionListAdd QuoteObj)
        {
            if (QuoteObj.quotation_itemsobj != null)
            {
                AuthDetails authdet = LoginUserDetails();
                foreach (var quote in QuoteObj.quotation_itemsobj)
                {
                    quote.UpdatedBy = authdet.UserId;
                    quote.CreatedBy = authdet.UserId;
                }
                var result = service.AddItems(QuoteObj.quotation_itemsobj);
                return Ok(result);
            }
            else
            {
                return Ok(0);
            }

        }

        QuotationServices service1 = new QuotationServices();
        DatatableService datatableService1 = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(QuotationSearch search)
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