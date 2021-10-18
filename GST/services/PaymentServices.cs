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
    public class PaymentServices : iCRUD<payment>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<PaymentDatatable> GetList(PaymentSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<PaymentDatatable>("payment_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(payment paymentData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.payment.FirstOrDefault(f => f.Id == paymentData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(paymentData);
                    }
                    else
                    {
                        ctx.payment.Add(paymentData);
                    }
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DownloadReceipt(payment paymentData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    string data = string.Empty;
                    TemplateService templateService = new TemplateService();
                    var tokens = templateService.GetTokensByModulName("PaymentReceipt");

                    var objPay = ctx.payment.FirstOrDefault(f => f.Id == paymentData.Id);

                    customer customer;
                    var plotDetails = ctx.plot.First(f => f.Id == objPay.PlotID);
                    if (plotDetails != null)
                    {
                        customer = ctx.customer.FirstOrDefault(f => f.PlotID == plotDetails.Id);
                    }
                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "AIM DEVLOPERD-Payment Receipt");
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


        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public payment Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.payment.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

    }
}
