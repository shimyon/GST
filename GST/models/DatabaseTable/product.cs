using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("product")]
    public class product : BaseEntity
    {

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(10)]
        public string MasureofUnit { get; set; }

        public decimal DefultRate { get; set; }





    }
}
