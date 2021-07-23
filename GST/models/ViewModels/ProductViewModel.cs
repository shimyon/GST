using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string MasureofUnit { get; set; }

        public decimal DefultRate { get; set; }
    }

    public class ProductSearch : DataTableSearch
    {

    }

    public class ProductDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string ProductName { get; set; }

        public string MasureofUnit { get; set; }

        public decimal DefultRate { get; set; }
    }
}