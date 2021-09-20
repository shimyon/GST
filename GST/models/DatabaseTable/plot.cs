using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("plot")]
    public class plot : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }

        public int PlotNo { get; set; }
    }
}
