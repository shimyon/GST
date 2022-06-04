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

}