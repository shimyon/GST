using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class SiteViewModel
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string OwnerName { get; set; }
    }

    public class SiteSearch : DataTableSearch
    {

    }

    public class SiteDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string OwnerName { get; set; }
    }
}