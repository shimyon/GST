using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services;
using services.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace GST.Controllers
{
    public class PlotController : BaseApiController
    {
        PlotServices service = new PlotServices();
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();

        [HttpPost]
        public IHttpActionResult GetList(PlotSearch search)
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
        public IHttpActionResult PlotIDDropDownAll(int Id)
        {
            var getPlot = service.PlotIDDropDownAll(Id);
            return Ok(getPlot);
        }

        [HttpPost]
        public IHttpActionResult GetById(PlotViewModel obj)
        {
            var getPlot = service.Get(obj.Id);
            return Ok(getPlot);
        }

        [HttpPost]
        public IHttpActionResult AddData(plot plotobj)
        {
            AuthDetails authdet = LoginUserDetails();
            plotobj.UpdatedBy = authdet.UserId;
            plotobj.CreatedBy = authdet.UserId;
            var result = service.Add(plotobj);
            return Ok(result);
        }


        [HttpPost]
        public HttpResponseMessage DocumentDownload(plot plotObj)
        {
            string filename = "sample";
            var buffer = GetTemplate(plotObj);
            HttpResponseMessage response = PDFResponse(filename, buffer);
            return response;
        }


        [HttpPost]
        public IHttpActionResult EmailReceipt(plot plotObj)
        {
            string senderEmail = string.Empty;
            try
            {
                var customer = service.GetCustomerByPlotId(plotObj.Id);
                if (customer == null)
                {
                    return Ok("Customer not found!");
                }

                byte[] buffer = GetTemplate(plotObj);
                string paymentReceipt = Path.Combine(Path.GetTempPath(), plotObj.DocumentType + ".pdf");
                File.WriteAllBytes(paymentReceipt, buffer);
                senderEmail = customer.Email;
                MailSettingViewModel settings = new MailSettingViewModel
                {
                    Subject = "Document",
                    Body = "Dear Sir/Madam<br><br><br>Your property document attached with this email. <br> Please find attachment.<br><br>Thank you.",
                    ToMailId = customer.Email,
                    ToMailName = customer.CustomerName
                };
                settings.AttchPath.Add(paymentReceipt);
                services.Common.EmailService.SendMail(settings);

                return Ok("Email sent successfully on '" + senderEmail + "' email id");
            }
            catch (Exception ex)
            {
                return Ok("Error occurred while in sending mail to " + senderEmail + Environment.NewLine + " Error Details:" + ex.ToString());
            }
        }


        private byte[] GetTemplate(plot plotObj)
        {
            var example_html = "<html><body></body></html>";
            var example_css = @"
                    body { 
                        padding: 20px 30px;
                        padding-left: 35px;
                        font-size: 14px;
                        font-family:""Calibri, sans-serif"";
                    }
                    .headline{font-size:200%}, 
                    td,th{ padding: 2px; }
                    ";
            string filename = "Sample";
            if (plotObj.DocumentType.Contains("Allotment Letter"))
            {
                var result = service.DownloadAllotmentLetter(plotObj);
                example_html = "<html><body>" + result + "</body></html>";
                example_css = @".headline{font-size:200%}";
            }
            else if (plotObj.DocumentType.Contains("Banakhat"))
            {
                var result = service.DownloadBanakhat(plotObj);
                example_html = "<html><body>" + result + "</body></html>";
                example_css = @".headline{font-size:200%}";
            }
            else if (plotObj.DocumentType.Contains("Sale Deed"))
            {
                var result = service.DownloadSaleDeed(plotObj);
                example_html = "<html><body>" + result + "</body></html>";
                example_css = @".headline{font-size:200%}";
            }
            byte[] buffer = commsrv.PdfGenerate(example_html, example_css);
            return buffer;
        }

    }

}