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
    public class ContactServices : iCRUD<contact>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<ContactDatatable> GetList(ContactSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<ContactDatatable>("contact_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(contact contactData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.contact.FirstOrDefault(f => f.Id == contactData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(contactData);
                    }
                    else
                    {
                        ctx.contact.Add(contactData);
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


       public contact Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.contact.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }
    }
}
