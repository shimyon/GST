using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services;
using services.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class FileUploadController : ApiController
    {
        FileUploadServices service = new FileUploadServices();
        CommonService comm = new CommonService();
        DatatableService datatableService = new DatatableService();
        //[HttpPost]
        //public IHttpActionResult GetList(CompanySearch search)
        //{
        //    AuthDetails authdet = LoginUserDetails();
        //    var filters = new List<MySqlParameter>
        //    {
        //        datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32)
        //    };
        //    var result = service.GetList(search, filters);
        //    return Ok(result);
        //}

        //[HttpPost]
        //public IHttpActionResult GetCompany(CompanyViewModel login)
        //{
        //    AuthDetails authDet = LoginUserDetails();
        //    Int32 id = authDet.id;
        //    var getCompany = service.Get(id);
        //    return Ok(getCompany);
        //}

        //[HttpGet]
        //public IHttpActionResult CompanyDropDownAll()
        //{
        //    var getCompany = service.CompanyDropDownAll();
        //    return Ok(getCompany);
        //}

        //[HttpPost]
        //public IHttpActionResult GetById(CompanyViewModel obj)
        //{
        //    var getCompany = service.Get(obj.Id);
        //    return Ok(getCompany);
        //}

        [HttpPost]
        public IHttpActionResult AddData(FileUploadViewModel fileUpload)
        {
            AuthDetails authdet = LoginUserDetails();
            companyobj.UpdatedBy = authdet.UserId;
            companyobj.CreatedBy = authdet.UserId;
            var result = service.Add(companyobj);
            return Ok(result);
        }


    }
}