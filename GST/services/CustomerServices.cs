using GST.Controllers;
using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class CustomerServices : iCRUD<customer>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<CustomerDatatable> GetList(CustomerSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<CustomerDatatable>("customer_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(customer customerData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.customer.FirstOrDefault(f => f.Id == customerData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(customerData);
                    }
                    else
                    {
                        ctx.customer.Add(customerData);
                    }
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public customer Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.customer.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public object CustomerIDDropDownAll()
        {
            using (var db = new AppDb())
            {
                var data = db.customer.Select(s => new
                {
                    value = s.Id,
                    label = s.CustomerName
                }).ToList();
                return data;
            }
        }

    }
}
