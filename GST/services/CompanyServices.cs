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
    public class CompanyServices : iCRUD<company>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<CompanyDatatable> GetList(CompanySearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<CompanyDatatable>("company_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Add(company companyData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.company.FirstOrDefault(f => f.Id == companyData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(companyData);
                    }
                    else
                    {
                        ctx.company.Add(companyData);
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

        public List<KeyValueViewModel> CompanyDropDownAll()
        {
            using (var db = new AppDb())
            {
                var data = db.company.Select(s => new KeyValueViewModel
                {
                    value = s.Id,
                    label = s.CompanyName
                }).ToList() ;
                return data;
            }
        }
      
    }
}
