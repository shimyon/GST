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
    public class Invoice_itemsServices : iCRUD<invoice_items>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<Invoice_itemsDatatable> GetInvo_itemsList(Invoice_itemsSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<Invoice_itemsDatatable>("invoice_items_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int AddItems(List<invoice_items> invoice_items)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    int? InvoiceID = invoice_items.FirstOrDefault().InvoiceID;
                    if (InvoiceID != null)
                    {
                        List<invoice_items> invoItems = ctx.invoice_items.Where(w => w.InvoiceID == InvoiceID).ToList();
                        ctx.invoice_items.RemoveRange(invoItems);
                    }
                    ctx.invoice_items.AddRange(invoice_items);
                    ctx.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.invoice_items.FirstOrDefault(f => f.Id == Id);
                db.invoice_items.Remove(data);
                var result = db.SaveChanges();
                return result;
            }
        }


        public List<invoice_items> Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.invoice_items.Where(f => f.InvoiceID == Id).ToList();
                return data;
            }
        }

        invoice_items iCRUD<invoice_items>.Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.invoice_items.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public int Add(invoice_items obj)
        {
            using (var db = new AppDb())
            {
                db.invoice_items.Add(obj);
                var data = db.SaveChanges();
                return data;
            }
        }
    }
}
