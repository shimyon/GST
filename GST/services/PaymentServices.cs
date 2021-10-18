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
                    Dictionary<string, string> tokens = templateService.GetTokensByModulName("Payment");
                    tokens["Common.CurrentDate"] = DateTime.Now.ToString("dd-MM-yyyy");

                    var objPay = ctx.payment.FirstOrDefault(f => f.Id == paymentData.Id);
                    if (objPay != null)
                    {
                        tokens["Payment.Id"] = Convert.ToString(objPay.Id);
                        tokens["Payment.Amount"] = Convert.ToString(objPay.Amount);
                        tokens["Payment.ChaqueueNo"] = Convert.ToString(objPay.ChequeNo);
                        tokens["Payment.ChaqueueDate"] = objPay.ChequeDateformate;
                        tokens["Payment.Drawn.On"] = objPay.DateOfIssueformate;
                        tokens["Payment.Bank"] = objPay.Bank;
                        tokens["Payment.Part"] = objPay.Part;
                        tokens["Payment.Amount.word"] = NumberToWords(objPay.Amount ?? 0).ToUpperInvariant() + " Only/-";
                    }

                    customer customer;
                    var plotDetails = ctx.plot.First(f => f.Id == objPay.PlotID);
                    if (plotDetails != null)
                    {
                        tokens["Payment.Unit.No"] = plotDetails.PlotNo;
                        customer = ctx.customer.FirstOrDefault(f => f.PlotID == plotDetails.Id);
                        tokens["Payment.Customer"] = customer.CustomerName;


                        var siteDetails = ctx.site.First(f => f.Id == plotDetails.SiteID);
                        Random random = new Random();
                        int randomNumber = random.Next(0, 1000);
                        tokens["Site.Logo"] = "Content/Images/SiteLogos/" + plotDetails.SiteID + ".png?v=" + randomNumber;
                        tokens["Site.Address"] = siteDetails.Address;
                        tokens["Site.Developer"] = siteDetails.Developer;
                        tokens["Site.WebSite"] = siteDetails.WebSite;
                    }
                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "AIM DEVLOPERD-Payment Receipt");
                    if (template != null)
                    {
                        data = ReplaceToken(template.TemplateData, tokens);
                    }


                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ReplaceToken(string html, Dictionary<string, string> tokens)
        {
            foreach (var item in tokens)
            {
                html = html.Replace("{{" + item.Key + "}}", item.Value);
            }
            return html;
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
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
