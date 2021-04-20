using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{

    public class DatatableCommon
    {
        public bool? IsDelete { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedDateFormat
        {
            get
            {
                if (CreatedDate.HasValue)
                {
                    return CreatedDate.Value.ToString("ddd, MMM d, yyyy hh:mm tt");
                }
                return string.Empty;
            }
        }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

    public class DataTable<T>
    {
        public List<T> data { get; set; }
        public int draw { get; set; }
        /// <summary>
        /// Current Page Records
        /// </summary>
        public int recordsTotal { get; set; }
        /// <summary>
        /// Total Page Records
        /// </summary>
        public double? recordsFiltered { get; set; }
    }

    public abstract class DataTableSearch
    {
        public int draw { get; set; }
        public Column[] columns { get; set; }
        public object[] order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class Column
    {
        public int data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search1 search { get; set; }
    }

    public class Search1
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }
}
