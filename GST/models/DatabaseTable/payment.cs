using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("payment")]
    public class payment : BaseEntity
    {
        public int? PlotID { get; set; }

        //public string Name { get; set; }

        public string ChequeNo { get; set; }

        public int? Amount { get; set; }

        [StringLength(50)]
        public string Bank { get; set; }

        [StringLength(15)]
        public string Part { get; set; }


        [NotMapped]
        public string SiteName { get; set; }

        public DateTime? DateOfIssue { get; set; }

        [NotMapped]
        public string DateOfIssueformate
        {
            get
            {
                if (DateOfIssue.HasValue)
                {
                    return DateOfIssue.Value.ToString("dd-MM-yyyy");
                }
                return string.Empty;
            }
        }

        public DateTime? ChequeDate { get; set; }

        [NotMapped]
        public string ChequeDateformate
        {
            get
            {
                if (ChequeDate.HasValue)
                {
                    return ChequeDate.Value.ToString("dd-MM-yyyy");
                }
                return string.Empty;
            }
        }

    }
}
