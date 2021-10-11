﻿using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public int? SiteID { get; set; }

        public int? PlotID { get; set; }

        public string CustomerName { get; set; }

        public int Age { get; set; }

        public string AdharCard { get; set; }

        public string PANCard { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
    }

    public class CustomerSearch : DataTableSearch
    {

    }

    public class CustomerDatatable : DatatableCommon
    {
        public int Id { get; set; }

        public int? SiteID { get; set; }

        public string SiteName { get; set; }

        public int? PlotID { get; set; }

        public string PlotNo { get; set; }

        public string CustomerName { get; set; }

        public int Age { get; set; }

        public string AdharCard { get; set; }

        public string PANCard { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
    }
}