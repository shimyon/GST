using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        public string PlotID { get; set; }

        public int Amount { get; set; }

        //public string Name { get; set; }

        public int ChequeNo { get; set; }

        public string Bank { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public DateTime? ChequeDate { get; set; }
    }

    public class PaymentSearch : DataTableSearch
    {

    }

    public class PaymentDatatable : DatatableCommon
    {
        public int Id { get; set; }

        public int? PlotID { get; set; }

        public string PlotNo { get; set; }

        public int Amount { get; set; }

        //public string Name { get; set; }

        public int ChequeNo { get; set; }

        public string Bank { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public string DateOfIssueformate
        {
            get
            {
                if (DateOfIssue.HasValue)
                {
                    return DateOfIssue.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        public DateTime? ChequeDate { get; set; }

        public string ChequeDateformate
        {
            get
            {
                if (ChequeDate.HasValue)
                {
                    return ChequeDate.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }
    }
}