using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class QuotationViewModel
    {
        public int Id { get; set; }
           
        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public DateTime QuotationDate { get; set; }
    }

    public class QuotationSearch : DataTableSearch
    {

    }

    public class QuotationDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public DateTime QuotationDate { get; set; }
    }
}

