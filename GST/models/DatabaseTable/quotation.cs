using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("quotation")]
    public class quotation : BaseEntity
    {
        public int? CompanyID { get; set; }

        public int? ContactID { get; set; }

        public DateTime QuotationDate { get; set; }

        [NotMapped]
        public string QuotationDateformate
        {
            get
            {
                return QuotationDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
