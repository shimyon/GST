using models.DatabaseTable;
using models.ViewModels;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class CompanyController : BaseApiController
    {
        CompanyServices service = new CompanyServices();

        //[HttpPost]
        //public IHttpActionResult GetCompany(CompanyViewModel login)
        //{
        //    AuthDetails authDet = LoginUserDetails();
        //    Int32 id = authDet.id;
        //    var getCompany = service.Get(id);
        //    return Ok(getCompany);
        //}

        [HttpGet]
        public IHttpActionResult CompanyDropDownAll()
        {
            var getCompany = service.CompanyDropDownAll();
            return Ok(getCompany);
        }

        [HttpPost]
        public IHttpActionResult GetById(CompanyViewModel obj)
        {
            var getCompany = service.Get(obj.Id);
            return Ok(getCompany);
        }

        [HttpPost]
        public IHttpActionResult AddData(company companyobj)
        {
            AuthDetails authdet = LoginUserDetails();
            companyobj.UpdatedBy = authdet.UserId;
            companyobj.CreatedBy = authdet.UserId;
            var result = service.Add(companyobj);
            return Ok(result);
        }


    }
}