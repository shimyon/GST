﻿using GST.Controllers;
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
    public class PlotServices : iCRUD<plot>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<PlotDatatable> GetList(PlotSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<PlotDatatable>("plot_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(plot plotData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.plot.FirstOrDefault(f => f.Id == plotData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(plotData);
                    }
                    else
                    {
                        ctx.plot.Add(plotData);
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

        public plot Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.plot.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public object PlotIDDropDownAll(int id)
        {
            using (var db = new AppDb())
            {
                var data = db.plot.Where(w => w.SiteID == id).Select(s => new
                {
                    value = s.Id,
                    label = s.PlotNo,
                    SellAmount = s.SellAmount
                }).ToList();
                return data;
            }
        }

        public string AddPlots(List<plot> plots)
        {
            try
            {
                using (var db = new AppDb())
                {
                    plots.ForEach(f =>
                    {
                        f.SellAmount = 10000;
                        f.Installments = 10;
                    });
                    db.plot.AddRange(plots);
                    db.SaveChanges();
                }
                return plots.Count + " plots records were added";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

    }
}
