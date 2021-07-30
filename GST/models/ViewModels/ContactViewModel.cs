using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public int CompanyID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public int Phone1 { get; set; }

        public int Phone2 { get; set; }

        public string Email { get; set; }
    }

    public class ContactSearch : DataTableSearch
    {

    }

    public class ContactDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string CompanyID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }
    }
}