using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("company")]
    public class company : BaseEntity
    {
        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Phone1 { get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string GSTNo { get; set; }
    }
}
