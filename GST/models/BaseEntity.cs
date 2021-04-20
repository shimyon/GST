using models.DatabaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public abstract class BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        public bool? IsDelete { get; set; }

        public Int32? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int32? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual user CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual user UpdatedByUser { get; set; }

        public BaseEntity()
        {
            IsDelete = false;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
