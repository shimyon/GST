using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class ChartViewModel
    {

        public List<int> Year { get; set; }
        public List<decimal> AmountReceived { get; set; }
        public List<int> Book { get; set; }

    }

    public class ChartSearch : DataTableSearch
    {
        public string rdate { get; set; }
        public string todate { get; set; }

    }
}
