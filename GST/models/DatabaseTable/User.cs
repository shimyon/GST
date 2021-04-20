using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace models.DatabaseTable
{
    [Table("users")]
    public class user
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }
        
        [StringLength(20)]
        public string Username { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        
        public bool? IsDelete { get; set; }

        public Int32? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int32? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public user()
        {
            IsDelete = false;
            //CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }

}