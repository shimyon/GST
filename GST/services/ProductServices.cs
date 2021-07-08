using models.DatabaseTable;
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
                        ctx.product.Add(obj);
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

    }
}
