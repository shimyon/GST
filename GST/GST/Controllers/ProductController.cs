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
    public class ProductController : BaseApiController
    {
        ProductServices service = new ProductServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(ProductSearch search)
        {
            AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
            };
            var result = service.GetList(search, filters);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult ProductDropDownAll()
        {
            var getProduct = service.ProductDropDownAll();
            return Ok(getProduct);
        }

        [HttpPost]
        public IHttpActionResult GetById(ProductViewModel obj)
        {
            var getProduct = service.Get(obj.Id);
            return Ok(getProduct);
        }

        [HttpPost]
        public IHttpActionResult AddData(product productobj)
        {
            AuthDetails authdet = LoginUserDetails();
            productobj.UpdatedBy = authdet.UserId;
            productobj.CreatedBy = authdet.UserId;
            var result = service.Add(productobj);
            return Ok(result);
        }
    }

}