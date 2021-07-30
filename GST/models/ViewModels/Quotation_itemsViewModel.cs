using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class Quotation_itemsViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string HSNCode { get; set; }

        public int Quantity { get; set; }

        public int Rate { get; set; }

        public int Discount { get; set; }

        public int Tax { get; set; }

        public int Total { get; set; }
    }

}