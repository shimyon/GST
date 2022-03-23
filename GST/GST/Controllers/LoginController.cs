using models.ViewModels;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace GST.Controllers
{
    public class LoginController : ApiController
    {
        UserService service = new UserService();
        CommonService comm = new CommonService();
        EmailService email = new EmailService();

        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public IHttpActionResult SignUp(LoginDetailsViewModel login)
        {
            bool isValidEmail = comm.IsValidEmail(login.Email);
            if (!isValidEmail)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid Email address");
            }

            if (!login.IsForgotPass)
            {
                String passErrMsg = String.Empty;
                bool isValidPass = comm.ValidatePassword(login.Password, out passErrMsg);
                if (!isValidPass)
                {
                    return Content(HttpStatusCode.BadRequest, passErrMsg);
                }
            }

            try
            {
                //string pass = Membership.GeneratePassword(4, 0);
                string pass = login.Password;
                services.Common.PasswordCryptoService crypto = new services.Common.PasswordCryptoService();
                string encpass = crypto.EncryptText(pass);
                if (login.IsForgotPass)
                {
                    service.UpdatePass(new models.DatabaseTable.user
                    {
                        Email = login.Email,
                        Username = login.Email,
                        Password = encpass
                    });
                }
                else
                {
                    service.Add(new models.DatabaseTable.user
                    {
                        Email = login.Email,
                        Username = login.Email,
                        Password = encpass,
                        CreatedDate = DateTime.Now
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
