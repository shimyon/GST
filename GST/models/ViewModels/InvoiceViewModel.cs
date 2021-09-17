using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int Total { get; set; }
    }

    public class InvoiceSearch : DataTableSearch
    {

    }

    public class InvoiceDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int Total { get; set; }
    }
}

