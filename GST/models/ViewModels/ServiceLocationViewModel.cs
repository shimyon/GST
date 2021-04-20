using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{

    public class ServiceLocationSearch : DataTableSearch
    {
    }

    public class ServiceLocationDatatable : DatatableCommon
    {
        public Int32 ServiceLocationId { get; set; }
        public string facilityName { get; set; }
           public string NPI { get; set; }
        public string TIN { get; set; }
        public string CLIA { get; set; }
        public string TypeofBill { get; set; }
        public string ColorTag { get; set; }
        public string Abr { get; set; }

    }

}
