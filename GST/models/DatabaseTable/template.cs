using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("template")]
    public class template : BaseEntity
    {
        //[ForeignKey("user")]
        public Int32 userid { get; set; }
        //public virtual user user { get; set; }

        [StringLength(10)]
        public string TemplateFor { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        public string TemplateData { get; set; }
    }
}
