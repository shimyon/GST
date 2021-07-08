using models.DatabaseTable;
using services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class CompanyServices : iCRUD<company>
    {
        public int Add(company companyData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.contact.FirstOrDefault(f => f.Id == companyData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(companyData);
                    }
                    else
                    {
                        ctx.contact.Add(obj);
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

        public company Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.company.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }
      
    }
}
