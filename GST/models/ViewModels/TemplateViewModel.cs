﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class TemplateViewModel
    {
        public int UserId { get; set; }
        
        public string TemplateName { get; set; }

        public string TemplateFor { get; set; }

        public string TemplateData { get; set; }
    }
}
