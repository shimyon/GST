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

        public DataTable<SiteOwnerDatatable> GetOwnerList(SiteSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<SiteOwnerDatatable>("site_owner_list_sp", search, filters);
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
                        ctx.site.Attach(obj);
                        obj.SiteName = sitetData.SiteName;
                        obj.Address = sitetData.Address;
                        obj.OwnerName = sitetData.OwnerName;
                        obj.Developer = sitetData.Developer;
                        obj.WebSite = sitetData.WebSite;
                        //ctx.Entry(obj).CurrentValues.SetValues(sitetData);
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

        public int AddOwner(site_owner siteidtData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.site_owner.FirstOrDefault(f => f.Id == siteidtData.Id);
                    if (obj != null)
                    {
                        ctx.site_owner.Attach(obj);
                        obj.SiteId = siteidtData.SiteId;
                        obj.SiteOwnerName = siteidtData.SiteOwnerName;
                        obj.AdharCard = siteidtData.AdharCard;
                        obj.PANCard = siteidtData.PANCard;
                        obj.IsMainOwner = siteidtData.IsMainOwner;
                        if (obj.IsMainOwner == true)
                        {
                            var objOldOwner = ctx.site_owner.FirstOrDefault(f => f.Id != siteidtData.Id && f.IsMainOwner == true);
                            if (objOldOwner != null)
                            {
                                ctx.site_owner.Attach(objOldOwner);
                                obj.IsMainOwner = null;
                            }
                        }
                        //ctx.Entry(obj).CurrentValues.SetValues(sitetData);
                    }
                    else
                    {
                        ctx.site_owner.Add(siteidtData);
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
            using (var db = new AppDb())
            {
                var data = db.site_owner.FirstOrDefault(f => f.Id == Id);
                if (data != null)
                {
                    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                    return db.SaveChanges();
                }
                return 0;
            }
        }

        public site Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.site.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public site_owner GetBySiteId(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.site_owner.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public site_owner GetOwner(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.site_owner.FirstOrDefault(f => f.SiteId == Id);
                return data;
            }
        }


        public List<site_owner> GetOwnerListBySiteId(int SiteId)
        {
            using (var db = new AppDb())
            {
                var data = db.site_owner.Where(f => f.SiteId == SiteId).OrderByDescending(o => o.IsMainOwner).ToList();
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
