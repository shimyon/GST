using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services;
using services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class SiteController : BaseApiController
    {
        SiteServices service = new SiteServices();
        DatatableService datatableService = new DatatableService();
        [HttpPost]
        public IHttpActionResult GetList(SiteSearch search)
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
        public IHttpActionResult SiteNameDropDownAll()
        {
            var getSite = service.SiteNameDropDownAll();
            return Ok(getSite);
        }

        [HttpPost]
        public IHttpActionResult GetById(SiteViewModel obj)
        {
            var getSite = service.Get(obj.Id);
            return Ok(getSite);
        }

        [HttpPost]
        public IHttpActionResult AddData(site siteobj)
        {
            AuthDetails authdet = LoginUserDetails();
            siteobj.UpdatedBy = authdet.UserId;
            siteobj.CreatedBy = authdet.UserId;
            var result = service.Add(siteobj);
            return Ok(result);
        }

        [HttpPost]
        public HttpResponseMessage UploadLogo()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    int hasheddate = DateTime.Now.GetHashCode();
                    //Good to use an updated name always, since many can use the same file name to upload.
                    string changed_name = hasheddate.ToString() + "_" + postedFile.FileName;
                    changed_name = "logo.png";
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + changed_name);
                    postedFile.SaveAs(filePath); // save the file to a folder "Images" in the root of your app

                    changed_name = @"~\Images\" + changed_name; //store this complete path to database
                    docfiles.Add(changed_name);

                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;

        }
    }

}