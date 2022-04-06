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
using LINQtoCSV;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace GST.Controllers
{
    public class SiteController : BaseApiController
    {
        SiteServices service = new SiteServices();
        PlotServices plotservice = new PlotServices();
        CommonService commsrv = new CommonService();
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

        [HttpPost]
        public IHttpActionResult GetOwnerList(SiteSearch search)
        {
            //AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pSiteId", search.SiteId,  MySqlDbType.Int32)
            };
            var result = service.GetOwnerList(search, filters);
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
        public IHttpActionResult AddOwnerData(site_owner siteidobj)
        {
            AuthDetails authdet = LoginUserDetails();
            siteidobj.UpdatedBy = authdet.UserId;
            siteidobj.CreatedBy = authdet.UserId;
            var result = service.AddOwner(siteidobj);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GetBySiteId(SiteOwnerViewModel obj)
        {
            var getSite = service.GetBySiteId(obj.Id);
            return Ok(getSite);
        }

        [HttpPost]
        public IHttpActionResult GetOwnerListBySiteId(SiteOwnerViewModel obj)
        {
            var owners = service.GetOwnerListBySiteId(obj.SiteId);
            return Ok(owners);
        }

        [HttpPost]
        public IHttpActionResult Delete(SiteOwnerDatatable obj)
        {
            var ownerdel = service.Delete(obj.Id);
            return Ok(ownerdel);
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
                    List<string> extionList = new List<string> { ".csv" };
                    if (extionList.Contains(extension.ToLower()))
                    {
                        string temppath = Path.Combine(Path.GetTempPath(), postedFile.FileName);
                        postedFile.SaveAs(temppath);
                        CsvFileDescription inputFileDescription = new CsvFileDescription
                        {
                            IgnoreUnknownColumns = true,
                            SeparatorChar = ',',
                            FirstLineHasColumnNames = true
                        };
                        using (TextFieldParser parser = new TextFieldParser(temppath))
                        {
                            parser.TextFieldType = FieldType.Delimited;
                            parser.SetDelimiters(",");
                            while (!parser.EndOfData)
                            {
                                //Processing row
                                string[] fields = parser.ReadFields();
                                foreach (string field in fields)
                                {
                                    //TODO: Process field
                                }
                            }
                        }
                        LINQtoCSV.CsvContext cc = new LINQtoCSV.CsvContext();
                        IEnumerable<Dictionary<string, string>> plotsList1 = cc.Read<Dictionary<string, string>>(temppath, inputFileDescription);
                        IEnumerable<PlotCSV> plotsList = cc.Read<PlotCSV>(temppath, inputFileDescription);
                        //var count = products.Count();
                        //var book = new LinqToExcel.ExcelQueryFactory(temppath);
                        //var book = new CsvHelper.CsvContext(temppath);
                        int siteId = int.Parse(httpRequest.Form["siteid"]);
                        int MaintenanceAmount = 0;

                        var query = from row in plotsList
                                    let item = new plot
                                    {
                                        SiteID = siteId,
                                        SellAmount = 10000,
                                        Installments = 10,
                                        CreatedBy = userDet.UserId,
                                        UpdatedBy = userDet.UserId,
                                        Floor = row.Floor,
                                        PlotNo = row.UnitNo,
                                        CarpetArea = row.CarpetArea,
                                        ConstructionArea = row.ConstructionArea,
                                        UndividedLand = row.UndividedLand,
                                        SuperBuildUp = row.SuperBuildUp,
                                        UndividedLandCommArea = row.UndividedLandCommArea,
                                        ProportionateLand = row.ProportionateLand,
                                        DirectionsNorth = row.DirectionsNorth,
                                        DirectionsSouth = row.DirectionsSouth,
                                        DirectionsEast = row.DirectionsEast,
                                        DirectionsWest = row.DirectionsWest,
                                        MaintenanceAmount = int.TryParse(row.MaintenanceAmount, out MaintenanceAmount) ? Convert.ToInt32(row.MaintenanceAmount) : 0
                                    }
                                    select item;
                        var listplot = query.ToList();
                        uploaded = plotservice.AddPlots(listplot);
                    }
                    else
                    {
                        uploaded = "Only following file will be uploaded: .csv";
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