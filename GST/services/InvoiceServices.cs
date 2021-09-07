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
    public class InvoiceServices : iCRUD<invoice>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<InvoiceDatatable> GetList(InvoiceSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<InvoiceDatatable>("invoice_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(invoice invoiceData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.invoice.FirstOrDefault(f => f.Id == invoiceData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(invoiceData);
                    }
                    else
                    {
                        ctx.invoice.Add(invoiceData);
                    }
                    ctx.SaveChanges();
                    return invoiceData.Id;

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


        public invoice Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.invoice.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }
    }
}
