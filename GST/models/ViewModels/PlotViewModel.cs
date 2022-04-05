﻿using models.ViewModels;
using System;

namespace GST.Controllers
{
    public class PlotViewModel
    {
        public Int32 Id { get; set; }

        public string SiteName { get; set; }

        public int PlotNo { get; set; }

        public int CarpetArea { get; set; }

        public int ConstructionArea { get; set; }

        public int UndividedLand { get; set; }

        public int UndividedLandCommArea { get; set; }

        public int ProportionateLand { get; set; }

        public string Floor { get; set; }

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

        public String CarpetArea { get; set; }

        public String ConstructionArea { get; set; }

        public String UndividedLand { get; set; }

        public String UndividedLandCommArea { get; set; }

        public String ProportionateLand { get; set; }

        public string Floor { get; set; }

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

    public class PlotCSV
    {
        public string Floor { get; set; }
        public string UnitNo { get; set; }
        public string CarpetArea { get; set; }
        public string Undevided { get; set; }
        public string ConstructionArea { get; set; }
        public string UndividedLand { get; set; }
        public string SuperBuildUp { get; set; }
        public string ProportionateLand { get; set; }
        public string DirectionsNorth { get; set; }
        public string DirectionsSouth { get; set; }
        public string DirectionsEast { get; set; }
        public string DirectionsWest { get; set; }
        public string MaintenanceAmount { get; set; }
        public string UndividedLandCommArea { get; set; }
    }
}