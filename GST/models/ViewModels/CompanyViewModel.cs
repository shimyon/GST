using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class CompanyViewModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public int Phone1 { get; set; }

        public int Phone2 { get; set; }

        public string Email { get; set; }

        public string GSTNo { get; set; }

        public string PANNo { get; set; }
    }

    public class CompanySearch : DataTableSearch
    {

    }

    public class CompanyDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public string GSTNo { get; set; }

        public string PANNo { get; set; }
    }
}