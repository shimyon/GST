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
    public class QuotationServices : iCRUD<quotation>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<QuotationDatatable> GetList(QuotationSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<QuotationDatatable>("quotation_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(quotation quotationData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.quotation.FirstOrDefault(f => f.Id == quotationData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(quotationData);
                    }
                    else
                    {
                        ctx.quotation.Add(quotationData);
                    }
                     ctx.SaveChanges();
                    return quotationData.Id;
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


       public quotation Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.quotation.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }
    }
}
