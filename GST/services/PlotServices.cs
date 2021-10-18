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

        public string DownloadAllotmentLetter(plot plotData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    string data = string.Empty;
                    TemplateService templateService = new TemplateService();
                    var tokens = templateService.GetTokensByModulName("PaymentReceipt");

                    List<customer> customer;
                    var plotDetails = ctx.plot.First(f => f.Id == plotData.Id);
                    if (plotDetails != null)
                    {
                        customer = ctx.customer.Where(f => f.PlotID == plotDetails.Id).ToList();
                    }

                    var objPay = ctx.payment.Where(f => f.PlotID == plotDetails.Id).ToList();

                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "Allotment Letter");
                    if (template != null)
                    {
                        data = template.TemplateData;
                    }


                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DownloadBanakhat(plot plotData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    string data = string.Empty;
                    TemplateService templateService = new TemplateService();
                    var tokens = templateService.GetTokensByModulName("Banakhat");

                    List<customer> customer;
                    var plotDetails = ctx.plot.First(f => f.Id == plotData.Id);
                    if (plotDetails != null)
                    {
                        customer = ctx.customer.Where(f => f.PlotID == plotDetails.Id).ToList();
                    }

                    var objPay = ctx.payment.Where(f => f.PlotID == plotDetails.Id).ToList();
                    
                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "ONE WEST-Banakhat");
                    if (template != null)
                    {
                        data = template.TemplateData;
                    }


                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DownloadSaleDeed(payment paymentData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    string data = string.Empty;
                    TemplateService templateService = new TemplateService();
                    var tokens = templateService.GetTokensByModulName("Sale Deed");

                    var objPay = ctx.payment.FirstOrDefault(f => f.Id == paymentData.Id);

                    customer customer;
                    var plotDetails = ctx.plot.First(f => f.Id == objPay.PlotID);
                    if (plotDetails != null)
                    {
                        customer = ctx.customer.FirstOrDefault(f => f.PlotID == plotDetails.Id);
                    }
                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "ONE WEST-Sale Deed");
                    if (template != null)
                    {
                        data = template.TemplateData;
                    }


                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
