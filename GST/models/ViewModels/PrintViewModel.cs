using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class PrintViewModel
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public string TemplateFor { get; set; }

        public string TemplateData { get; set; }
    }
}