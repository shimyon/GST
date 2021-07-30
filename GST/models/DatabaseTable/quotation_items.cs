using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("quotation_items")]
    public class quotation_items : BaseEntity
    {
        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string HSNCode { get; set; }

        public int Quantity { get; set; }

        public int Rate { get; set; }

        public int Discount { get; set; }

        public int Tax { get; set; }

        public int Total { get; set; }
    }
}
