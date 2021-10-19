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

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string OwnerName { get; set; }

        [StringLength(50)]
        public string Developer { get; set; }

        [StringLength(50)]
        public string WebSite { get; set; }
    }
}
