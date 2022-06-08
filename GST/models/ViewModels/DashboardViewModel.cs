using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class DashboardViewModel
    {
        public decimal AmountReceived { get; set; }

        public int Book { get; set; }

        public int Unbook { get; set; }

        public int Dastavej { get; set; }

        public int Banakhat { get; set; }
    }
    public class DashboardSearch: DataTableSearch
    {
        public string rdate { get; set; }
        public string todate { get; set; }
        public string rdays { get; set; }
     
    }
}