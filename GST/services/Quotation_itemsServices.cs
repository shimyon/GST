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
    public class Quotation_itemsServices : iCRUD<quotation_items>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();


        public int Add(quotation_items quotation_itemsData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.quotation_items.FirstOrDefault(f => f.Id == quotation_itemsData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(quotation_itemsData);
                    }
                    else
                    {
                        ctx.quotation_items.Add(quotation_itemsData);
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


       public quotation_items Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.quotation_items.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }
    }
}
