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
        public int? SellAmount { get; set; }
    }
}
