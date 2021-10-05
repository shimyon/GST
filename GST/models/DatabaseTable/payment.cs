﻿using System;
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
        [StringLength(50)]
        public string PlotID { get; set; }

        //public string Name { get; set; }

        public int ChequeNo { get; set; }

        public int Amount { get; set; }

        [StringLength(50)]
        public string Bank { get; set; }

        public DateTime? DateOfIssue { get; set; }

        [NotMapped]
        public string DateOfIssueformate
        {
            get
            {
                if (DateOfIssue.HasValue)
                {
                    return DateOfIssue.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        public DateTime? ChequeDate { get; set; }

        [NotMapped]
        public string ChequeDateformate
        {
            get
            {
                if (ChequeDate.HasValue)
                {
                    return ChequeDate.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

    }
}
