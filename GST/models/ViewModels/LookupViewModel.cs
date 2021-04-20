using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class LookupSearch: DataTableSearch
    {
    }

    public class LookupDatatable : DatatableCommon
    {
        public string Class { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}
