using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{

    public class UserSearch : DataTableSearch
    {
    }

    public class UserDatatable : DatatableCommon
    {
        public Int32 UserId { get; set; }

        public string IDNumber { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        //public string City { get; set; }

    }
}
