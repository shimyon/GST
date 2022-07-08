using models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;

namespace GST.Controllers
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        public AuthDetails LoginUserDetails()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            AuthDetails authdet = new AuthDetails();
            var claim = claims.Where(w => w.Type == "UserId").FirstOrDefault();
            if (claim != null)
            {
                authdet.UserId = int.Parse(claim.Value);
            }
            claim = claims.Where(w => w.Type == "Username").FirstOrDefault();
            if (claim != null)
            {
                authdet.UserName = claim.Value;
            }
            return authdet;
        }

        public HttpResponseMessage PDFResponse(string filename, byte[] buffer)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            var statuscode = HttpStatusCode.OK;
            response = Request.CreateResponse(statuscode);
            response.Content = new StreamContent(new MemoryStream(buffer));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentLength = buffer.Length;
            ContentDispositionHeaderValue contentDisposition = null;
            //inline
            if (ContentDispositionHeaderValue.TryParse("inline; filename=" + filename + ".pdf", out contentDisposition))
            {
                response.Content.Headers.ContentDisposition = contentDisposition;
            }
            return response;
        }

        public HttpResponseMessage DOCResponse(string filename, byte[] buffer)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            var statuscode = HttpStatusCode.OK;
            response = Request.CreateResponse(statuscode);
            response.Content = new StreamContent(new MemoryStream(buffer));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/doc");
            response.Content.Headers.ContentLength = buffer.Length;
            ContentDispositionHeaderValue contentDisposition = null;
            //inline
            if (ContentDispositionHeaderValue.TryParse("inline; filename=" + filename + ".doc", out contentDisposition))
            {
                response.Content.Headers.ContentDisposition = contentDisposition;
            }
            return response;
        }


        public string PDFbase64String(byte[] pdfByteArray)
        {
            string base64EncodedPDF = System.Convert.ToBase64String(pdfByteArray);
            return base64EncodedPDF;
        }



    }
}
