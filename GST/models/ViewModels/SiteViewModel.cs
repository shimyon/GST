using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class SiteViewModel
    {
        public Int32 Id { get; set; }

        public Int32 SiteId { get; set; }

        public string SiteName { get; set; }

        public string SiteOwnerName { get; set; }

        public string AdharCard { get; set; }

        public string PANCard { get; set; }

        public string Address { get; set; }

        public string OwnerName { get; set; }

        public string Developer { get; set; }

        public string WebSite { get; set; }
    }

    public class SiteOwnerViewModel
    {
        public Int32 Id { get; set; }

        public Int32 SiteId { get; set; }

        public string SiteOwnerName { get; set; }

        public string AdharCard { get; set; }

        public string PANCard { get; set; }

    }

    public class SiteSearch : DataTableSearch
    {
        public Int32 Id { get; set; }

        public Int32 SiteId { get; set; }

        public string SiteOwnerName { get; set; }

        public string AdharCard { get; set; }

        public string PANCard { get; set; }
    }

    public class SiteDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string SiteName { get; set; }

        public string Address { get; set; }

        public string OwnerName { get; set; }

        public string Developer { get; set; }

        public string WebSite { get; set; }
    }

    public class SiteOwnerDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public Int32 SiteId { get; set; }

        public string SiteOwnerName { get; set; }

        public string AdharCard { get; set; }

        public string PANCard { get; set; }

        public bool? IsMainOwner { get; set; }
    }
}