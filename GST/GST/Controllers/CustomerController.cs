﻿using models.DatabaseTable;
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
    public class CustomerController : BaseApiController
    {
        CustomerServices service = new CustomerServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(CustomerSearch search)
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
        public IHttpActionResult CustomerIDDropDownAll()
        {
            var getCustomer = service.CustomerIDDropDownAll();
            return Ok(getCustomer);
        }



        [HttpPost]
        public IHttpActionResult GetById(CustomerViewModel obj)
        {
            var getCustomer = service.Get(obj.Id);
            return Ok(getCustomer);
        }

        [HttpPost]
        public IHttpActionResult AddData(customer customerobj)
        {
            AuthDetails authdet = LoginUserDetails();
            customerobj.UpdatedBy = authdet.UserId;
            customerobj.CreatedBy = authdet.UserId;
            var result = service.Add(customerobj);
            return Ok(result);
        }
    }

}