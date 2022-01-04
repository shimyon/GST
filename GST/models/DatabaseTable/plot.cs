using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("plot")]
    public class plot : BaseEntity
    {
        public int SiteID { get; set; }

        [StringLength(50)]
        public String Floor { get; set; }
        
        [StringLength(50)]
        public String PlotNo { get; set; }

        [StringLength(50)]
        public string ConstructionArea { get; set; }


        [StringLength(50)]
        public string UndividedLand { get; set; }

        [StringLength(50)]
        public string UndividedLandCommArea { get; set; }

        [StringLength(50)]
        public string CarpetArea { get; set; }

        [StringLength(50)]
        public string SuperBuildUp { get; set; }

        [StringLength(50)] 
        public string ProportionateLand { get; set; }

        [StringLength(50)]
        public string DirectionsNorth { get; set; }

        [StringLength(50)]
        public string DirectionsSouth { get; set; }

        [StringLength(50)]
        public string DirectionsEast { get; set; }

        [StringLength(50)]
        public string DirectionsWest { get; set; }

        public int? MaintenanceAmount { get; set; }

        public int SellAmount { get; set; }

        public int Installments { get; set; }


        [NotMapped]
        public string DocumentType { get; set; }

        [StringLength(50)]
        public string RegNo { get; set; }

        public DateTime? RegDate { get; set; }

        [NotMapped]
        public string RegDateformate
        {
            get
            {
                if (RegDate.HasValue)
                {
                    return RegDate.Value.ToString("dd-MM-yyyy");
                }
                return string.Empty;
            }
        }

        public DateTime? AllotmentLtDt { get; set; }

        [NotMapped]
        public string AllotmentLtDtformate
        {
            get
            {
                if (AllotmentLtDt.HasValue)
                {
                    return AllotmentLtDt.Value.ToString("dd-MM-yyyy");
                }
                return string.Empty;
            }
        }

        public DateTime? TitleClearFrom { get; set; }

        [NotMapped]
        public string TitleClearFromformate
        {
            get
            {
                if (TitleClearFrom.HasValue)
                {
                    return TitleClearFrom.Value.ToString("dd-MM-yyyy");
                }
                return string.Empty;
            }
        }

        public DateTime? TitleClearDt { get; set; }

        [NotMapped]
        public string TitleClearDtformate
        {
            get
            {
                if (TitleClearDt.HasValue)
                {
                    return TitleClearDt.Value.ToString("dd-MM-yyyy");
                }
                return string.Empty;
            }
        }

        public string Bank { get; set; }
    }
}
