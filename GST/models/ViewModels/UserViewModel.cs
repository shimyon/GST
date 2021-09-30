using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

    }

    public class UserSearch : DataTableSearch
    {

    }

    public class UserDatatable : DatatableCommon
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
