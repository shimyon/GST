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
        public String PlotNo { get; set; }

        [StringLength(50)]
        public string SquareArea { get; set; }

        [StringLength(50)]
        public string SuperBuildUp { get; set; }

        [StringLength(50)]
        public string DirectionsNorth { get; set; }

        [StringLength(50)]
        public string DirectionsSouth { get; set; }

        [StringLength(50)]
        public string DirectionsEast { get; set; }

        [StringLength(50)]
        public string DirectionsWest { get; set; }

        public int SellAmount { get; set; }

        public int Installments { get; set; }


        [NotMapped]
        public string DocumentType { get; set; }
    }
}
