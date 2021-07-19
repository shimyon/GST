using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("contact")]
    public class contact : BaseEntity
    {
        public int? CompanyID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string DOB { get; set; }

        [StringLength(15)]
        public string Phone1 { get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
