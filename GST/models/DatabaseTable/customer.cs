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

        [StringLength(50)]
        public String PlotID { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public int Age { get; set; }

        public int AdharCard { get; set; }

        [StringLength(50)]
        public string PANCard { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public int Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
