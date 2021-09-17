using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        public int PlotID { get; set; }

        public int CustomerID { get; set; }

        public int ChequeNo { get; set; }

        public string Bank { get; set; }

        public DateTime Date { get; set; }

        public int SINo { get; set; }
    }

    public class PaymentSearch : DataTableSearch
    {

    }

    public class PaymentDatatable : DatatableCommon
    {
        public int Id { get; set; }

        public int PlotID { get; set; }

        public int CustomerID { get; set; }

        public int ChequeNo { get; set; }

        public string Bank { get; set; }

        public DateTime Date { get; set; }

        public int SINo { get; set; }
    }
}