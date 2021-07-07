using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class LoginDetailsViewModel
    {
        public string Email { get; set; }
        public bool IsForgotPass { get; set; }
        public string Password { get; set; }
    }
}
