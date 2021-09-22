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
        [StringLength(50)]
        public string SiteName { get; set; }

        public int PlotNo { get; set; }

        public int SquareArea { get; set; }

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

    }
}
