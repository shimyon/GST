using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("customer")]
    public class customer : BaseEntity
    {
        public int SiteID { get; set; }

        public int PlotID { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public int Age { get; set; }

        [StringLength(100)] 
        public string Occupation { get; set; }

        [StringLength(50)]
        public string AdharCard { get; set; }

        [StringLength(50)]
        public string PANCard { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [NotMapped]
        public string RegNo { get; set; }

        [NotMapped]
        public int? SellAmount { get; set; }

        [NotMapped]
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

        [NotMapped]
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
        
        [NotMapped]
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

        [NotMapped]
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

    }
}
