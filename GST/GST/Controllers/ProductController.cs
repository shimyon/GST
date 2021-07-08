﻿using models.ViewModels;
using services;
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

        [HttpPost]
        public IHttpActionResult GetProduct(ProductViewModel obj)
        {
            var getProduct = service.Get(obj.Id);
            return Ok(getProduct);
        }
    }
}