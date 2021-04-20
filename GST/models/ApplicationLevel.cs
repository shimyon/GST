using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public static class ApplicationLevel
    {
        public static string DateFormat {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(a => a == "DateFormat"))
                {
                    return ConfigurationManager.AppSettings["DateFormat"];
                }
                return "dd/MM/yyyy";
            }
            
        }

        static ApplicationLevel()
        {
         
        }
    }
}
