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
    public class PaymentController : BaseApiController
    {
        PaymentServices service = new PaymentServices();
        DatatableService datatableService = new DatatableService();
        CommonService commsrv = new CommonService();
        [HttpPost]
        public IHttpActionResult GetList(PaymentSearch search)
        {
            //AuthDetails authdet = LoginUserDetails();
            var filters = new List<MySqlParameter>
            {
                datatableService.CreateSqlParameter("@pPlotId", search.PlotId,  MySqlDbType.Int32)
            };
            var result = service.GetList(search, filters);
            return Ok(result);
        }
        
        [HttpPost]
        public IHttpActionResult GetById(PaymentViewModel obj)
        {
            var getPayment = service.Get(obj.Id);
            return Ok(getPayment);
        }

        [HttpPost]
        public IHttpActionResult AddData(payment paymentobj)
        {
            AuthDetails authdet = LoginUserDetails();
            paymentobj.UpdatedBy = authdet.UserId;
            paymentobj.CreatedBy = authdet.UserId;
            var result = service.Add(paymentobj);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Delete(payment obj)
        {
            var objnew = service.Delete(obj.Id);
            return Ok(objnew);
        }

        [HttpPost]
        public HttpResponseMessage DownloadReceipt(payment paymentobj)
        {
            string filename = "Sample";
            //paymentobj.SiteName.Contains("Payment");
            var result = service.DownloadReceipt(paymentobj);
            var example_html = "<html><body>" + result + "</body></html>";
            byte[] buffer = GetTemplate(paymentobj, example_html);
            HttpResponseMessage response = PDFResponse(filename, buffer);
            return response;
        }

        [HttpPost]
        public IHttpActionResult EmailReceipt(payment paymentobj)
        {
            string senderEmail = string.Empty;
            try
            {
                var customer = service.GetCustomerByPlotId(paymentobj.Id);
                if (customer == null)
                {
                    return Ok("Customer not found!");
                }
                var result = service.DownloadReceipt(paymentobj);
                var example_html = "<html><body>" + result + "</body></html>";


                byte[] buffer = GetTemplate(paymentobj, example_html);
                string paymentReceipt = Path.Combine(Path.GetTempPath(), "payment_receipt.pdf");
                File.WriteAllBytes(paymentReceipt, buffer);
                senderEmail = customer.Email;
                MailSettingViewModel settings = new MailSettingViewModel
                {
                    Subject = "Payment Receipt",
                    Body = example_html,
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


        private byte[] GetTemplate(payment paymentobj, string example_html)
        {
            var result = service.DownloadReceipt(paymentobj);
            var example_css = "";
            byte[] buffer = commsrv.PdfGenerate(example_html, example_css);
            return buffer;
        }

    }

}