using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("invoice")]
    public class invoice : BaseEntity
    {
        public int CompanyID { get; set; }

        public int ContactID { get; set; }

        public DateTime InvoiceDate { get; set; }

        [NotMapped]
        public string InvoiceDateformate
        {
            get
            {
                return InvoiceDate.ToString("yyyy-MM-dd");
            }
        }

        public int Total { get; set; }
    }
}
