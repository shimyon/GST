using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("site")]
    public class site : BaseEntity
    {
        [StringLength(50)]
        public string SiteName { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Developer { get; set; }

        [StringLength(100)]
        public string WebSite { get; set; }


        [StringLength(50)]
        public string OwnerName { get; set; }

        public Int16? BanakhatId { get; set; }
        public Int16? SaleDeedId { get; set; }
        public Int16? AllotmentId { get; set; }
        public Int16? PaymentId { get; set; }


    }

    [Table("site_owner")]
    public class site_owner : BaseEntity
    {
        public Int16? SiteId { get; set; }
        public string SiteOwnerName { get; set; }
        public string AdharCard { get; set; }
        public string PANCard { get; set; }
        public bool? IsMainOwner { get; set; }

    }
}
