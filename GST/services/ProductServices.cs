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
    public class ProductServices : iCRUD<product>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<ProductDatatable> GetList(ProductSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<ProductDatatable>("product_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(product productData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.product.FirstOrDefault(f => f.Id == productData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(productData);
                    }
                    else
                    {
                        ctx.product.Add(productData);
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

        public product Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.product.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public object ProductDropDownAll()
        {
            using (var db = new AppDb())
            {
                var data = db.product.Select(s => new
                {
                    value = s.Id,
                    label = s.ProductName,
                    s.DefultRate
                }).ToList();
                return data;
            }
        }

    }
}
