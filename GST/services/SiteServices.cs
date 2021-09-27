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
    public class SiteServices : iCRUD<site>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<SiteDatatable> GetList(SiteSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<SiteDatatable>("site_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(site sitetData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.site.FirstOrDefault(f => f.Id == sitetData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(sitetData);
                    }
                    else
                    {
                        ctx.site.Add(sitetData);
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

        public site Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.site.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public object SiteNameDropDownAll()
        {
            using (var db = new AppDb())
            {
                var data = db.site.Select(s => new
                {
                    value = s.Id,
                    label = s.SiteName
                }).ToList();
                return data;
            }
        }

    }
}
