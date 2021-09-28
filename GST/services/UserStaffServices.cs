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
    public class UserStaffServices : iCRUD<userStaff>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<UserStaffDatatable> GetList(UserStaffSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<UserStaffDatatable>("user_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(userStaff userStaffData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.userStaff.FirstOrDefault(f => f.Id == userStaffData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(userStaffData);
                    }
                    else
                    {
                        ctx.userStaff.Add(userStaffData);
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

        public userStaff Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.userStaff.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

    }     
}
