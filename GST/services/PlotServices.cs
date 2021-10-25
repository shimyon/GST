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
                    Dictionary<string, string> tokens = TokenData(plotData.Id, "Payment");

                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "Allotment Letter");
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


        public string DownloadBanakhat(plot plotData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    string data = string.Empty;

                    Dictionary<string, string> tokens = TokenData(plotData.Id, "Banakhat");

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

        public string DownloadSaleDeed(plot plotData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    string data = string.Empty;
                    var tokens = TokenData(plotData.Id, "Sale Deed");

                    var template = ctx.template.FirstOrDefault(f => f.TemplateName == "ONE WEST-Sale Deed");
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

        public Dictionary<string, string> TokenData(int plotId, string tokenTemplate)
        {
            TemplateService templateService = new TemplateService();
            Dictionary<string, string> tokens = templateService.GetTokensByModulName(tokenTemplate);
            using (var ctx = new AppDb())
            {
                tokens["Common.CurrentDate"] = DateTime.Now.ToString("dd-MM-yyyy");

                var plotDetails = ctx.plot.First(f => f.Id == plotId);
                if (plotDetails != null)
                {
                    tokens["Payment.Floor"] = plotDetails.Floor;
                    tokens["Payment.Unit.No"] = plotDetails.PlotNo;
                    tokens["Payment.CarpetArea"] = plotDetails.CarpetArea;
                    tokens["Payment.ConstructionArea"] = plotDetails.ConstructionArea;
                    tokens["Payment.UndividedLand"] = plotDetails.UndividedLand;
                    tokens["Payment.SuperBuildUp"] = plotDetails.SuperBuildUp;
                    tokens["Payment.DirectionsNorth"] = plotDetails.DirectionsNorth;
                    tokens["Payment.DirectionsSouth"] = plotDetails.DirectionsSouth;
                    tokens["Payment.DirectionsEast"] = plotDetails.DirectionsEast;
                    tokens["Payment.DirectionsWest"] = plotDetails.DirectionsWest;
                    tokens["Payment.RegNo"] = plotDetails.RegNo;
                    tokens["Payment.RegDate"] = plotDetails.RegDate.HasValue ? plotDetails.RegDate.Value.ToString("dd-MM-yyyy") : "";
                    tokens["Payment.AllotmentLtDt"] = plotDetails.AllotmentLtDt.HasValue ? plotDetails.AllotmentLtDt.Value.ToString("dd-MM-yyyy") : "";
                    tokens["Payment.TitleClearFrom"] = plotDetails.TitleClearFrom.HasValue? plotDetails.TitleClearFrom.Value.ToString("dd-MM-yyyy") : "";
                    tokens["Payment.TitleClearDt"] = plotDetails.TitleClearDt.HasValue? plotDetails.TitleClearDt.Value.ToString("dd-MM-yyyy") : "";

                    tokens["Sale.Amount"] = plotDetails.SellAmount.ToString("#.##");
                    double outstanding = plotDetails.SellAmount - (plotDetails.SellAmount * 0.10);
                    tokens["Amount.Outstanding"] = outstanding.ToString("#.##");
                    tokens["Amount.10"] = (plotDetails.SellAmount * 0.10).ToString("#.##");
                    tokens["Amount.30"] = (plotDetails.SellAmount * 0.20).ToString("#.##");
                    tokens["Amount.45"] = (plotDetails.SellAmount * 0.15).ToString("#.##");
                    tokens["Amount.70"] = (plotDetails.SellAmount * 0.25).ToString("#.##");

                    double slabAmount = (plotDetails.SellAmount * 0.25);

                    tokens["Amount.FirstSlab"] = (slabAmount * 0.40).ToString("#.##");
                    tokens["Amount.ThirdSlab"] = (slabAmount * 0.40).ToString("#.##");
                    tokens["Amount.FifthSlab"] = (slabAmount * 0.20).ToString("#.##");


                    tokens["Amount.75"] = (plotDetails.SellAmount * 0.5).ToString("#.##");
                    tokens["Amount.80"] = (plotDetails.SellAmount * 0.5).ToString("#.##");
                    tokens["Amount.85"] = (plotDetails.SellAmount * 0.5).ToString("#.##");
                    tokens["Amount.95"] = (plotDetails.SellAmount * 0.10).ToString("#.##");
                    tokens["Amount.5"] = (plotDetails.SellAmount * 0.5).ToString("#.##");

                    List<customer> customers = ctx.customer.Where(f => f.PlotID == plotDetails.Id).ToList();
                    string customerDetails = "<table  cellspacing ='5' cellpadding='5' border='1' style='width:80%; border-collapse: collapse; margin : 20px 10px;'>";
                    foreach (var item in customers.Select((val, i) => new { val, i }))
                    {
                        customerDetails += "<tr><td>" + (item.i + 1) + ".</td><td>";

                        customerDetails += "<table cellspacing ='5' cellpadding='5'>";
                        customerDetails += "<tr>";
                        customerDetails += "<td>" + item.val.CustomerName + ", Aged: Adult (" + item.val.Age + " years), Occuption:" + item.val.Occupation + "</td>";
                        customerDetails += "</tr><tr>";
                        customerDetails += "<td>PAN: " + item.val.PANCard + ", ADHAR CARD:" + item.val.AdharCard + "</td>";
                        customerDetails += "</tr><tr>";
                        customerDetails += "<td>Email ID: " + item.val.Email + "</td>";
                        customerDetails += "</tr></table>";

                        customerDetails += "</td></tr>";
                    }
                    customerDetails += "</table>";

                    tokens["Customer.Details"] = customerDetails;

                    var customer = customers.FirstOrDefault();
                    tokens["Payment.Customer"] = customer.CustomerName;
                    tokens["Customer.Name"] = customer.CustomerName;
                    tokens["Customer.Age"] = Convert.ToString(customer.Age);
                    tokens["Customer.Address"] = Convert.ToString(customer.Address);
                    tokens["Customer.PANNo"] = Convert.ToString(customer.PANCard);
                    tokens["Customer.AdharNo"] = Convert.ToString(customer.AdharCard);
                    tokens["Customer.Email"] = Convert.ToString(customer.Email);


                    var siteDetails = ctx.site.First(f => f.Id == plotDetails.SiteID);
                    Random random = new Random();
                    int randomNumber = random.Next(0, 1000);
                    tokens["Site.Logo"] = "Content/Images/SiteLogos/" + plotDetails.SiteID + ".png?v=" + randomNumber;
                    tokens["Site.Address"] = siteDetails.Address;
                    tokens["Site.Developer"] = siteDetails.Developer;
                    tokens["Site.WebSite"] = siteDetails.WebSite;
                }

                var objPay = ctx.payment.FirstOrDefault(f => f.PlotID == plotId);
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
                string paymentTable = @"<table border='1' style='width:80%; border-collapse:collapse; margin : 10px 10px;''><tr><th>Sr.No.</th> <th>Amount (Rs.)</th> <th>Cheque No.</th> <th>Cheque Date</th> <th>Bank</th></tr>";

                double totPay = 0;
                var objPayList = ctx.payment.Where(f => f.PlotID == plotId).ToList();
                foreach (var item in objPayList.Select((val, i) => new { val, i }))
                {
                    totPay += item.val.Amount ?? 0;
                    paymentTable += "<tr> <th>" + (item.i + 1) + "</th> <th>" + item.val.Amount + "</th> <th>" + item.val.ChequeNo + "</th> <th>" + item.val.ChequeDateformate + "</th> <th>" + item.val.Bank + "</th> </tr>";
                }
                paymentTable += "</table>";
                tokens["Payment.Table"] = paymentTable;
                tokens["Payment.TotalPayment"] = Convert.ToString(totPay);
            }
            return tokens;
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

            if ((number / 10000000) > 0)
            {
                words += NumberToWords(number / 10000000) + " crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " lakh ";
                number %= 100000;
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

    }
}
