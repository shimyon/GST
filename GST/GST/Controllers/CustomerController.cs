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
                datatableService.CreateSqlParameter("@pUserId", authdet.UserId,  MySqlDbType.Int32),
                datatableService.CreateSqlParameter("@pName", search.Name,  MySqlDbType.VarChar),
                datatableService.CreateSqlParameter("@pMobile", search.Mobile,  MySqlDbType.VarChar),
                datatableService.CreateSqlParameter("@pShop", search.Shop,  MySqlDbType.VarChar)
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
        public IHttpActionResult GetAllByPlotId(CustomerViewModel obj)
        {
            var getCustomer = service.GetAllByPlotId(obj.PlotID);
            return Ok(getCustomer);
        }


        [HttpPost]
        public IHttpActionResult Delete(customer obj)
        {
            var objnew = service.Delete(obj.Id);
            return Ok(objnew);
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
        public HttpResponseMessage uploaddocument()
        {
            string acceptFiles = ConfigurationManager.AppSettings["acceptfiles"];
            if (!ConfigurationManager.AppSettings.AllKeys.Any(a => a == "acceptfiles"))
            {
                throw new Exception("acceptfiles not defined in web.config file.");
            }

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                string uploaded = "Uploaded";
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string extension = System.IO.Path.GetExtension(postedFile.FileName);
                    List<string> extionList = acceptFiles.Split(',').ToList();
                    if (extionList.Contains(extension.ToLower()))
                    {
                        int hasheddate = DateTime.Now.GetHashCode();
                        //Good to use an updated name always, since many can use the same file name to upload.
                        string changed_name = hasheddate.ToString() + "_" + postedFile.FileName;
                        changed_name = httpRequest.Form["plotid"] + "_" + postedFile.FileName;
                        var filePath = HttpContext.Current.Server.MapPath("~/Content/images/CustomerDocument/" + changed_name);
                        postedFile.SaveAs(filePath); // save the file to a folder "Images" in the root of your app
                        uploaded = "Uploaded";
                    }
                    else
                    {
                        uploaded = "Only following file will be uploaded: .png, .jpg, .jpeg, .txt, .doc";
                    }
                }
                result = Request.CreateResponse(HttpStatusCode.Created, uploaded);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;

        }
    }

}