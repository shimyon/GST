using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("Invoice_items")]
    public class invoice_items : BaseEntity
    {
        public int? InvoiceID { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string HSNCode { get; set; }

        public int Quantity { get; set; }

        public int Rate { get; set; }

        public int Discount { get; set; }

        public int Tax { get; set; }

        public int Amount { get; set; }

        public int Total { get; set; }
    }
}
