using models.DatabaseTable;
using models.ViewModels;
using System;
using System.Collections.Generic;

namespace GST.Controllers
{
    public class Invoice_itemsViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string HSNCode { get; set; }

        public int Quantity { get; set; }

        public int Rate { get; set; }

        public int Discount { get; set; }

        public int Tax { get; set; }

        public int Amount { get; set; }

        public int Total { get; set; }
    }

    public class InvoiceListAdd
    {
        public List<invoice_items> invoice_itemsobj { get; set; }
    }
}

public class Invoice_itemsSearch : DataTableSearch
{

}

public class Invoice_itemsDatatable : DatatableCommon
{
    public Int32 Id { get; set; }

    public string ProductName { get; set; }

    public string HSNCode { get; set; }

    public int Quantity { get; set; }

    public int Rate { get; set; }

    public int Discount { get; set; }

    public int Tax { get; set; }

    public int Amount { get; set; }

    public int Total { get; set; }
}
