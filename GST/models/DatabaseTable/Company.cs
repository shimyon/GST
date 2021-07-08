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
        [StringLength(10)]
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public string GSTNo { get; set; }
    }
}
