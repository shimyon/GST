using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class MailSettingViewModel
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string ToMailId { get; set; }
        public string ToMailName { get; set; }
    }
}
