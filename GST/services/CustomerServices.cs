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
    public class CustomerServices : iCRUD<customer>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<CustomerDatatable> GetList(CustomerSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<CustomerDatatable>("customer_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(customer customerData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.customer.FirstOrDefault(f => f.Id == customerData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(customerData);
                    }
                    else
                    {
                        ctx.customer.Add(customerData);
                    }

                    var plot = ctx.plot.FirstOrDefault(f => f.Id == customerData.PlotID);
                    if (plot != null)
                    {
                        if (customerData.SellAmount != null)
                        {
                            plot.SellAmount = customerData.SellAmount ?? 0;
                        }
                        if (customerData.RegNo != null)
                        {
                            plot.RegNo = customerData.RegNo;
                        }
                        if (customerData.RegDate != null)
                        {
                            plot.RegDate = customerData.RegDate;
                        }
                        if (customerData.AllotmentLtDt != null)
                        {
                            plot.AllotmentLtDt = customerData.AllotmentLtDt;
                        }
                        if (customerData.TitleClearFrom != null)
                        {
                            plot.TitleClearFrom = customerData.TitleClearFrom;
                        }
                        if (customerData.TitleClearDt != null)
                        {
                            plot.TitleClearDt = customerData.TitleClearDt;
                        }
                        plot.Bank = customerData.Bank;
                        plot.Status = customerData.Status;

                    }
                    var intSave = ctx.SaveChanges();
                    return intSave;
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
                var data = db.customer.FirstOrDefault(f => f.Id == Id);
                if (data != null)
                {
                    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                    return db.SaveChanges();
                }
                return 0;
            }
        }

        public customer Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.customer.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public object GetAllByPlotId(int? PlotId)
        {
            using (var db = new AppDb())
            {
                var data = db.customer.Where(f => f.PlotID == PlotId).ToList();
                var plot = db.plot.FirstOrDefault(f => f.Id == PlotId);
                return new { customer = data, plot = plot };
            }
        }

        public object CustomerIDDropDownAll()
        {
            using (var db = new AppDb())
            {
                var data = db.customer.Select(s => new
                {
                    value = s.Id,
                    label = s.CustomerName
                }).ToList();
                return data;
            }
        }

    }
}
