using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services;
using services.Common;
using System;
using System.Collections.Generic;
using System.IO;
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
        PlotServices plotservice = new PlotServices();
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
                string uploaded = "Uploaded";
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string extension = System.IO.Path.GetExtension(postedFile.FileName);
                    List<string> extionList = new List<string> { ".png", ".jpg", ".jpeg" };
                    if (extionList.Contains(extension.ToLower()))
                    {
                        int hasheddate = DateTime.Now.GetHashCode();
                        //Good to use an updated name always, since many can use the same file name to upload.
                        string changed_name = hasheddate.ToString() + "_" + postedFile.FileName;
                        changed_name = httpRequest.Form["siteid"] + ".png";
                        var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/SiteLogos/" + changed_name);
                        postedFile.SaveAs(filePath); // save the file to a folder "Images" in the root of your app
                        uploaded = "Uploaded";
                    }
                    else
                    {
                        uploaded = "Only following file will be uploaded: .png, .jpg, jpeg";
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

        [HttpPost]
        public HttpResponseMessage UploadPlots()
        {
            var userDet = LoginUserDetails();
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                string uploaded = "Uploaded";
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string extension = System.IO.Path.GetExtension(postedFile.FileName);
                    List<string> extionList = new List<string> { ".xls", ".xlsx" };
                    if (extionList.Contains(extension.ToLower()))
                    {
                        string temppath = Path.Combine(Path.GetTempPath(), postedFile.FileName);
                        postedFile.SaveAs(temppath);
                        var book = new LinqToExcel.ExcelQueryFactory(temppath);
                        int siteId = int.Parse(httpRequest.Form["siteid"]);
                        var query =
                            from row in book.Worksheet(0)
                            let item = new plot
                            {
                                SiteID = siteId,
                                SellAmount = 10000,
                                Installments = 10,
                                CreatedBy = userDet.UserId,
                                UpdatedBy = userDet.UserId,
                                PlotNo = row["PlotNo"].Cast<string>(),
                                SquareArea = row["SquareArea"].Cast<string>(),
                                SuperBuildUp = row["SuperBuildUp"].Cast<string>(),
                                DirectionsNorth = row["DirectionsNorth"].Cast<string>(),
                                DirectionsSouth = row["DirectionsSouth"].Cast<string>(),
                                DirectionsEast = row["DirectionsEast"].Cast<string>(),
                                DirectionsWest = row["DirectionsWest"].Cast<string>()
                            }
                            select item;
                        var listplot = query.ToList();
                        uploaded = plotservice.AddPlots(listplot);

                    }
                    else
                    {
                        uploaded = "Only following file will be uploaded: .xls, .xlsx";
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