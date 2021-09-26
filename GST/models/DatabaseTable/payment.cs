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
        public int PlotNo { get; set; }

        public string Name { get; set; }

        public int ChequeNo { get; set; }

        [StringLength(50)]
        public string Bank { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public string Dateformate
        {
            get
            {
                return Date.ToString("yyyy-MM-dd");
            }
        }

        public int SINo { get; set; }
    }
}
