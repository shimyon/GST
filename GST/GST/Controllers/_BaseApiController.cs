using models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace GST.Controllers
{
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
    }
}
