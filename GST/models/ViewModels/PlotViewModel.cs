using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class PlotViewModel
    {
        public Int32 Id { get; set; }

        public string SiteName { get; set; }

        public int PlotNo { get; set; }

        public int SquareArea { get; set; }

        public string SuperBuildUp { get; set; }

        public string DirectionsNorth { get; set; }

        public string DirectionsSouth { get; set; }

        public string DirectionsEast { get; set; }

        public string DirectionsWest { get; set; }

        public int SellAmount { get; set; }

        public int Installments { get; set; }

        public string RegNo { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? AllotmentLtDt { get; set; }

        public DateTime? TitleClearFrom { get; set; }

        public DateTime? TitleClearDt { get; set; }
    }

    public class PlotSearch : DataTableSearch
    {

    }

    public class PlotDatatable : DatatableCommon
    {
        public Int32 Id { get; set; }

        public string SiteName { get; set; }

        public String PlotNo { get; set; }

        public String SquareArea { get; set; }

        public string SuperBuildUp { get; set; }

        public string DirectionsNorth { get; set; }

        public string DirectionsSouth { get; set; }

        public string DirectionsEast { get; set; }

        public string DirectionsWest { get; set; }

        public int SellAmount { get; set; }

        public int Installments { get; set; }

        public string RegNo { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? AllotmentLtDt { get; set; }

        public DateTime? TitleClearFrom { get; set; }

        public DateTime? TitleClearDt { get; set; }
    }
}