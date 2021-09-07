using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.DatabaseTable
{
    [Table("token")]
    public class token : BaseEntity
    {
        public string TokenType { get; set; }

        public string TokenName { get; set; }        
    }
}
